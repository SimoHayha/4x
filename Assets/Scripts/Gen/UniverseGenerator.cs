using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class UniverseInfo
{
    public int Size;
    public Hashtable Grid;

    public int SystemCount;
    public int AnomalyCount;
    public int BlackHoleCount;
}

public static class UniverseGenerator
{
    static List<CubeHex> Range(CubeHex center, int N)
    {
        //Debug.Log(center.Q() + " " + center.R() + " " + center.S());

        List<CubeHex> results = new List<CubeHex>();

        int dx;
        int dy;
        int dz;
        for (dx = -N; dx <= N; ++dx)
        {
            for (dy = Mathf.Max(-N, -dx - N); dy <= Mathf.Min(N, -dx + N); ++dy)
            {
                dz = -dx - dy;

                CubeHex cube = new CubeHex(dx, dy, dz);
                AxialHex a1 = HexUtils.CubeToAxialDirect(center);
                AxialHex a2 = HexUtils.CubeToAxialDirect(cube);
                //Debug.Log(a1 + " " + a2);

                results.Add(HexUtils.CubeAdd(center, new CubeHex(dx, dy, dz)));
            }
        }

        return results;
    }

    static void GenerateStars(UniverseInfo info)
    {
        int i = 0;
        Hashtable table = info.Grid.Clone() as Hashtable;
        Debug.Log(table.Count);
        while (table.Count > 0)
        {
            int rand = Random.Range(0, table.Count);
            HexGrid.GameHex[] arr = table.Values.Cast<HexGrid.GameHex>().ToArray();
            HexGrid.GameHex gh = arr[rand];

            //Debug.Log(gh.hex.Q() + " " + gh.hex.R());
            //Selection.activeTransform = gh.transform;

            gh.box = new Box();
            gh.box.Type = Box.BoxType.Star;
            gh.SetupBox();

            //Vector3 v = HexUtils.AxialToCube(gh.hex);
            Selection.activeTransform = gh.transform;
            CubeHex cubeHex = HexUtils.AxialToCubeDirect(gh.hex);
            List<CubeHex> inRange = Range(cubeHex, 5);
            Debug.Log("Range " + inRange.Count);
            foreach (CubeHex hex in inRange)
            {
                AxialHex axialHex = HexUtils.CubeToAxialDirect(hex);
                //Debug.Log(axialHex + " " + axialHex.GetHashCode() + " " + hex + " " + hex.GetHashCode());
                //HexGrid.GameHex gh2 = table[axialHex.GetHashCode()] as HexGrid.GameHex;
                //if (gh2 != null)
                //    gh2.mesh.renderer.material = null;
                table.Remove(axialHex.GetHashCode());
            }
            //break;


            Debug.Log(table.Count);

            //i++;
            //if (i > 3)
            //    break;
        }
    }

    public static void Generate(UniverseInfo info)
    {
        foreach (DictionaryEntry e in info.Grid)
        {
            HexGrid.GameHex gameHex = e.Value as HexGrid.GameHex;
            gameHex.box = new Box();
            GameObject.Destroy(gameHex.boxMesh);
        }

        GenerateStars(info);

        //for (int i = 0; i < info.SystemCount; ++i)
        //{
        //    int q = Random.Range(-info.Size, info.Size);
        //    int r1 = Mathf.Max(-info.Size, -q - info.Size);
        //    int r2 = Mathf.Min(info.Size, -q + info.Size);
        //    int r = Random.Range(r1, r2);

        //    AxialHex hex = new AxialHex(q, r);
        //    Debug.Log(q + " " + r);
        //    var gameHex = info.Grid[hex.GetHashCode()] as HexGrid.GameHex;
        //    gameHex.box = new Box();
        //    gameHex.box.Type = Box.BoxType.Star;
        //    gameHex.SetupBox();
        //}

        //for (int i = 0; i < info.BlackHoleCount; ++i)
        //{
        //    int q = Random.Range(-info.Size, info.Size);
        //    int r1 = Mathf.Max(-info.Size, -q - info.Size);
        //    int r2 = Mathf.Min(info.Size, -q + info.Size);
        //    int r = Random.Range(r1, r2);

        //    AxialHex hex = new AxialHex(q, r);
        //    var gameHex = info.Grid[hex.GetHashCode()] as HexGrid.GameHex;
        //    gameHex.box = new Box();
        //    gameHex.box.Type = Box.BoxType.BlackHole;
        //    gameHex.SetupBox();
        //}

        //for (int i = 0; i < info.AnomalyCount; ++i)
        //{
        //    int q = Random.Range(-info.Size, info.Size);
        //    int r1 = Mathf.Max(-info.Size, -q - info.Size);
        //    int r2 = Mathf.Min(info.Size, -q + info.Size);
        //    int r = Random.Range(r1, r2);

        //    AxialHex hex = new AxialHex(q, r);
        //    var gameHex = info.Grid[hex.GetHashCode()] as HexGrid.GameHex;
        //    gameHex.box = new Box();
        //    gameHex.box.Type = Box.BoxType.Anomaly;
        //    gameHex.SetupBox();
        //}
    }
}