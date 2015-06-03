using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;

public class HexGrid : MonoBehaviour
{
    public Camera CameraToUse;
    public Material LineMaterial;
    public Material HexMaterial;
    public Material SelectedHex;
    public int MapRadius = 10;
    public Vector3 Origin;
    public Transform GridTransform;

    Vector3[] points;
    VectorLine selectedPolygon;
    GameObject selectedObject;
    Layout layout;

    List<Hex> map;
    List<VectorLine> lines;
    List<GameHex> grid;
    GameObject hexMeshModel;

    //Hex[,] hexes;
    Hashtable hexes;

    Material mouseOver;
    GameHex over;

    List<CubeHex> inRange;

    public class GameHex : MonoBehaviour
    {
        public AxialHex hex;
        public GameObject mesh;
        public VectorLine line;
        public Collider2D collider;
        public int Hash;
        public Box box;
        public GameObject boxMesh;

        private TextMesh textMesh;

        void Start()
        {
            Hash = hex.GetHashCode();

            SetupBox();
            GameObject text = new GameObject("pos");
            text.transform.parent = transform;
            text.transform.localPosition = Vector3.zero;
            text.transform.localRotation = Quaternion.identity;
            textMesh = text.AddComponent<TextMesh>();
            Font arial = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textMesh.font = arial;
            textMesh.GetComponent<Renderer>().material = arial.material;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.characterSize = 0.25f;
            textMesh.text = hex.Q() + " " + hex.R();

            //Vector3 cubeCoord = HexUtils.AxialToCube(hex);
            //CubeHex cubeHex = HexUtils.AxialToCubeDirect(hex);
            //textMesh.text = cubeHex.Q() + " " + cubeHex.R() + " " + cubeHex.S();

            //Vector2 axialCoord = HexUtils.CubeToHex();
            //textMesh.text = axialCoord[0] + " " + axialCoord[1];
            //Vector3 cubeCoord = HexUtils.GetCubeCoord();
            //textMesh.text = cubeCoord[0] + " " + cubeCoord[1] + " " + cubeCoord[2];
        }

        public void SetupBox()
        {
            if (box == null)
            {
                box = new Box();
                box.Type = Box.BoxType.Space;
            }

            if (boxMesh == null)
            {
                if (box.Type == Box.BoxType.BlackHole)
                {
                    GameObject blackHoleModel = Resources.Load<GameObject>("Box/BlackHole");
                    boxMesh = Instantiate(blackHoleModel, transform.position, Quaternion.identity) as GameObject;
                }
                else if (box.Type == Box.BoxType.Star)
                {
                    GameObject blackHoleModel = Resources.Load<GameObject>("Box/Star");
                    boxMesh = Instantiate(blackHoleModel, transform.position, Quaternion.identity) as GameObject;
                }
                else if (box.Type == Box.BoxType.Anomaly)
                {
                    GameObject blackHoleModel = Resources.Load<GameObject>("Box/Anomaly");
                    boxMesh = Instantiate(blackHoleModel, transform.position, Quaternion.identity) as GameObject;
                }
            }

            if (boxMesh != null)
            {
                boxMesh.transform.parent = transform;
                boxMesh.transform.localPosition = Vector3.zero;
            }
        }
    }

    void Start()
    {
        LoadResources();

        hexes = new Hashtable();

        map = new List<Hex>();
        grid = new List<GameHex>();

        for (int q = -MapRadius; q <= MapRadius; ++q)
        {
            int r1 = Mathf.Max(-MapRadius, -q - MapRadius);
            int r2 = Mathf.Min(MapRadius, -q + MapRadius);
            for (int r = r1; r <= r2; ++r)
                map.Add(new AxialHex(q, r, -q - r));
        }

        //VectorLine.SetCanvasCamera(CameraToUse);
        VectorLine.SetCamera3D(CameraToUse);
        VectorLine.canvas.planeDistance = 10;

        VectorLine.canvas.renderMode = RenderMode.WorldSpace;
        VectorLine.canvas.GetComponent<RectTransform>().position = Vector3.zero;
        VectorLine.canvas.GetComponent<RectTransform>().localScale = new Vector3(0.02565f, 0.02565f, 0.0f);

        Orientation orientation = Orientation.LayoutPointy();
        //Orientation orientation = Orientation.LayoutFlat();
        layout = new Layout(orientation, new Vector3(1.0f, 1.0f, 0.0f), Origin);
        lines = new List<VectorLine>();

        CreateMesh();

        foreach (Hex h in map)
        {
            List<Vector3> points = HexUtils.PolygonCorner(layout, h);
            Vector3 center = FindCenter(points);
            points.Add(points[0]);
            //VectorLine line = new VectorLine("Line", points, LineMaterial, 2.0f, LineType.Continuous, Joins.Weld);
            //line.drawTransform = GridTransform;
            //line.drawDepth = 0;
            //line.Draw();

            //lines.Add(line);

            //GameObject gameObject = new GameObject("case");

            //Debug.Log(center);
            GameObject hexMesh = Instantiate(hexMeshModel/*, center, Quaternion.identity*/) as GameObject;

            GameHex gh = /*new GameHex(h, line);*/ hexMesh.AddComponent<GameHex>();
            gh.hex = h as AxialHex;
            //gh.line = line;
            gh.mesh = hexMesh;

            hexMesh.transform.eulerAngles = new Vector3(0.0f, 180.0f, 180.0f);
            hexMesh.transform.position = center;
            hexMesh.transform.parent = GridTransform;
            hexMesh.GetComponent<Renderer>().material = HexMaterial;
            hexMesh.name = "hexagon(" + gh.hex.Q() + "," + gh.hex.R() + ")";

            CreateCollider(layout, gh, hexMesh);

            hexes.Add(h.GetHashCode(), gh);

            grid.Add(gh);
        }

        //CreateSelectPolygon(layout);

        hexMeshModel.SetActive(false);

        UniverseInfo info = new UniverseInfo();
        info.AnomalyCount = 5;
        info.BlackHoleCount = 2;
        info.SystemCount = 8;
        info.Size = MapRadius;
        info.Grid = hexes;
        UniverseGenerator.Generate(info);

        GridTransform.localEulerAngles = new Vector3(0.0f, 180.0f, 180.0f);
    }

    private void LoadResources()
    {
        mouseOver = Resources.Load<Material>("Materials/MouseOver");
        inRange = new List<CubeHex>();
    }

    void CreateCollider(Layout layout, GameHex gh, GameObject obj)
    {
        List<Vector2> points = HexUtils.PolygonCorner2d(layout, gh.hex);
        for (int i = 0; i < points.Count; ++i)
            points[i] -= new Vector2(obj.transform.position.x, obj.transform.position.y);
        PolygonCollider2D polygonCollider = obj.AddComponent<PolygonCollider2D>();
        polygonCollider.points = points.ToArray();
    }

    Vector3 FindCenter(List<Vector3> points)
    {
        Vector3 center = Vector3.zero;
        foreach (Vector3 v3 in points)
        {
            center += v3;
        }
        return center / points.Count;
    }

    void CreateMesh()
    {
        hexMeshModel = new GameObject("Mesh");
        HexMesh hexMesh = hexMeshModel.AddComponent<HexMesh>();
        hexMesh.Create(layout);
    }

    void CreateSelectPolygon(Layout layout)
    {
        selectedObject = new GameObject("SelectedObject");
        List<Vector3> points = HexUtils.PolygonCorner(layout, new AxialHex(0, 0, 0));
        points.Add(points[0]);
        selectedPolygon = new VectorLine("SelectPolygon", points, SelectedHex, 1.0f, LineType.Continuous, Joins.Weld);
        selectedPolygon.drawTransform = selectedObject.transform;
        selectedPolygon.rectTransform.position = new Vector3(0.0f, 0.0f, -0.1f);
        selectedPolygon.Draw();
        //line.Draw();
    }

    void CreateCollider(Layout layout)
    {
        for (int i = 0; i < grid.Count; ++i)
        {
            GameHex gh = grid[i];

            List<Vector2> points = HexUtils.PolygonCorner2d(layout, gh.hex);
            gh.transform.parent = transform;
            gh.transform.position = Vector3.zero;
            PolygonCollider2D polygonCollider = gh.gameObject.AddComponent<PolygonCollider2D>();
            polygonCollider.points = points.ToArray();
            gh.collider = polygonCollider;
        }
    }

    void Update()
    {
        if (over != null)
            over.mesh.GetComponent<Renderer>().material = mouseOver;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = /*10.0f*/ - Camera.main.transform.position.z;
        //Vector2 point = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector2 camera = Camera.main.transform.position;
        //RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
        //Debug.DrawLine(point, Vector2.zero - camera);
        if (hit.collider != null)
        {
            GameHex gh = hit.transform.GetComponent<GameHex>();
            if (gh != null)
            {
                if (over != null)
                    over.mesh.GetComponent<Renderer>().material = HexMaterial;
                over = gh;
                over.mesh.GetComponent<Renderer>().material = mouseOver;
            }

            if (Input.GetMouseButtonDown(0))
            {
                foreach (CubeHex hex in inRange)
                {
                    Debug.Log(hex.GetHashCode());
                    GameHex ghInRange = hexes[hex.GetHashCode()] as GameHex;
                    if (ghInRange != null)
                        ghInRange.mesh.GetComponent<Renderer>().material = HexMaterial;
                }

                AxialHex center = new AxialHex(0, 0);
                int range = HexUtils.AxialDistance(center, gh.hex);

                inRange = Range(HexUtils.AxialToCubeDirect(gh.hex), 2);
                Debug.Log(inRange.Count);
                foreach (CubeHex hex in inRange)
                {
                    AxialHex axialHex = HexUtils.CubeToAxialDirect(hex);
                    GameHex ghInRange = hexes[axialHex.GetHashCode()] as GameHex;
                    if (ghInRange != null)
                        ghInRange.mesh.GetComponent<Renderer>().material = mouseOver;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UniverseInfo info = new UniverseInfo();
            info.AnomalyCount = 4;
            info.BlackHoleCount = 1;
            info.SystemCount = 2;
            info.Size = MapRadius;
            info.Grid = hexes;
            UniverseGenerator.Generate(info);
        }

    }

    List<CubeHex> Range(CubeHex center, int N)
    {
        List<CubeHex> results = new List<CubeHex>();

        int dx;
        int dy;
        int dz;
        for (dx = -N; dx <= N; ++dx)
        {
            for (dy = Mathf.Max(-N, -dx - N); dy <= Mathf.Min(N, -dx + N); ++dy)
            {
                dz = -dx - dy;
                results.Add(HexUtils.CubeAdd(center, new CubeHex(dx, dy, dz)));
            }
        }

        return results;
    }

    //void HighlightNeighbor(GameHex center)
    //{
    //    int distance = 2;
    //    int x;
    //    int y;
    //    int z;
    //    foreach (GameHex gh in grid)
    //    {
    //        x = gh.hex.Q() - center.hex.Q();
    //        y = gh.hex.R() - center.hex.R();
    //        z = gh.hex.S() - center.hex.S();
    //        if (-distance <= x && x <= distance)
    //        {
    //            if (-distance <= y && y <= distance)
    //            {
    //                if (-distance <= z && z <= distance)
    //                {
    //                    if (x + y + z == 0)
    //                        gh.line.material = SelectedHex;
    //                }
    //            }
    //        }
    //    }
    //}
}
