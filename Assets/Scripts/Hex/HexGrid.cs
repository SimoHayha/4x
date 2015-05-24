using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;

public class HexGrid : MonoBehaviour
{
    public Camera CameraToUse;
    public Material LineMaterial;
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

    public class GameHex : MonoBehaviour
    {
        public Hex hex;
        public VectorLine line;
        public Collider2D collider;
    }

    void Start()
    {
        map = new List<Hex>();
        grid = new List<GameHex>();

        for (int q = -MapRadius; q <= MapRadius; ++q)
        {
            int r1 = Mathf.Max(-MapRadius, -q - MapRadius);
            int r2 = Mathf.Min(MapRadius, -q + MapRadius);
            for (int r = r1; r <= r2; ++r)
                map.Add(new Hex(q, r, -q - r));
        }

        VectorLine.SetCanvasCamera(CameraToUse);
        VectorLine.canvas.planeDistance = 10;

        Orientation orientation = Orientation.LayoutPointy();
        layout = new Layout(orientation, new Vector3(0.5f, 0.5f, 0.0f), Origin);
        lines = new List<VectorLine>();

        foreach (Hex h in map)
        {
            List<Vector3> points = HexUtils.PolygonCorner(layout, h);
            points.Add(points[0]);
            VectorLine line = new VectorLine("Line", points, LineMaterial, 1.0f, LineType.Continuous, Joins.Weld);
            line.drawTransform = GridTransform;
            line.drawDepth = 0;
            line.Draw();

            lines.Add(line);

            GameObject gameObject = new GameObject("case");
            GameHex gh = /*new GameHex(h, line);*/ gameObject.AddComponent<GameHex>();
            gh.hex = h;
            gh.line = line;
            grid.Add(gh);
        }

        CreateCollider(layout);
        CreateSelectPolygon(layout);
    }

    void CreateSelectPolygon(Layout layout)
    {
        selectedObject = new GameObject("SelectedObject");
        List<Vector3> points = HexUtils.PolygonCorner(layout, new Hex(0, 0));
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
        //if (GridTransform.hasChanged)
        //{
        //    foreach (VectorLine line in lines)
        //        line.Draw();
        //}

        selectedPolygon.Draw();
        foreach (VectorLine line in lines)
            line.Draw();

        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //p.z = 0.0f;
            //Debug.Log(p);
            //Debug.DrawLine(Camera.main.transform.position, p, Color.green, 5.0f);
            //Vector3 mousePos = Input.mousePosition;
            //mousePos.z = 0.0f;
            //Vector3 p = Camera.main.ScreenToWorldPoint(mousePos);
            //p.z = 0.0f;
            //Debug.DrawLine(Camera.main.transform.position, p, Color.green, 5.0f);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            Debug.Log(mousePos);
            Vector2 point = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(point);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name);
                GameHex gh = hit.transform.GetComponent<GameHex>();
                if (gh != null)
                {
                    //Debug.Log("hello");
                    selectedObject.transform.position = HexUtils.HexToPixel(layout, gh.hex);

                    HighlightNeighbor(gh);
                }
            }
        }
    }

    void HighlightNeighbor(GameHex center)
    {
        int distance = 2;
        int x;
        int y;
        int z;
        foreach (GameHex gh in grid)
        {
            x = gh.hex.Q() - center.hex.Q();
            y = gh.hex.R() - center.hex.R();
            z = gh.hex.S() - center.hex.S();
            if (-distance <= x && x <= distance)
            {
                if (-distance <= y && y <= distance)
                {
                    if (-distance <= z && z <= distance)
                    {
                        if (x + y + z == 0)
                            gh.line.material = SelectedHex;
                    }
                }
            }
        }
    }
}
