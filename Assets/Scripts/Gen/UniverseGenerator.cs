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

    public List<HexGrid.GameHex> Stars = new List<HexGrid.GameHex>();
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
        Hashtable table = info.Grid.Clone() as Hashtable;
        int i = 0;
        while (table.Count > 0)
        {
            int rand = Random.Range(0, table.Count);
            HexGrid.GameHex[] arr = table.Values.Cast<HexGrid.GameHex>().ToArray();
            HexGrid.GameHex gh = arr[rand];
            i++;

            if (i > 50)
                break;

            if (gh.box.Type != Box.BoxType.Space)
                continue;
            gh.box.Type = Box.BoxType.Star;

            StarHandler handler = new StarHandler();
            handler.Type = StarHandler.StarType.YellowDwarf;
            handler.Texture = TextureLoader.StarTextures[Random.Range(0, TextureLoader.StarTextures.Length)];
            handler.CoronaTexture = TextureLoader.CoronaTextures[Random.Range(0, TextureLoader.CoronaTextures.Length)];
            gh.box.Handler = handler;
            gh.box.Handler.SetGUIHandler(new GUIStarHandler());
            gh.SetupBox();

            info.Stars.Add(gh);

            Selection.activeTransform = gh.transform;
            CubeHex cubeHex = HexUtils.AxialToCubeDirect(gh.hex);
            List<CubeHex> inRange = Range(cubeHex, 8);
            foreach (CubeHex hex in inRange)
            {
                AxialHex axialHex = HexUtils.CubeToAxialDirect(hex);
                table.Remove(axialHex.GetHashCode());
            }
        }
    }

    public static void Generate(UniverseInfo info)
    {
        foreach (DictionaryEntry e in info.Grid)
        {
            HexGrid.GameHex gameHex = e.Value as HexGrid.GameHex;
            gameHex.box = new Box();
            gameHex.box.Type = Box.BoxType.Space;
            gameHex.box.Handler = new GridHandler();
            gameHex.box.Handler.SetGUIHandler(new GUIGridHandler());
            GameObject.Destroy(gameHex.boxMesh);
        }

        // 1st pass
        GenerateStars(info);
        GenerateBlackHoles(info);
        GenerateAnomalies(info);

        // 2nd pass
        GeneratePlanets(info);

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

    private static void GeneratePlanets(UniverseInfo info)
    {
        foreach (HexGrid.GameHex gh in info.Stars)
        {
            int[] arr = new int[] { 1, 2, 3 };
            int nbPlanet = Random.Range(0, arr.Length);
            StarHandler starHandler = gh.box.Handler as StarHandler;

            for (int i = 0; i < nbPlanet; ++i)
            {
                int range = Random.Range(0, arr.Length);
                List<AxialHex> ring = HexUtils.AxialRing(gh.hex, arr[range]);
                List<HexGrid.GameHex> ghsInRing = new List<HexGrid.GameHex>();
                foreach (var hex in ring)
                {
                    HexGrid.GameHex ghInRing = info.Grid[hex.GetHashCode()] as HexGrid.GameHex;
                    if (ghInRing != null && ghInRing.box.Type == Box.BoxType.Space)
                        ghsInRing.Add(ghInRing);
                }

                Debug.Log(ghsInRing.Count);
                HexGrid.GameHex spot = ghsInRing[Random.Range(0, ghsInRing.Count)];

                PlanetHandler handler = new PlanetHandler();
                handler.System = starHandler.System;
                spot.box.Handler = handler;
                spot.box.Handler.SetGUIHandler(new GUIPlanetHandler());
                spot.box.Type = Box.BoxType.Planet;
                spot.SetupBox();

                starHandler.System.Planets.Add(handler);
            }
        }
    }

    private static void GenerateAnomalies(UniverseInfo info)
    {
        Hashtable table = info.Grid.Clone() as Hashtable;
        int i = 0;
        while (table.Count > 0)
        {
            int rand = Random.Range(0, table.Count);
            HexGrid.GameHex[] arr = table.Values.Cast<HexGrid.GameHex>().ToArray();
            HexGrid.GameHex gh = arr[rand];
            i++;

            if (i > 50)
                break;

            if (gh.box.Type != Box.BoxType.Space)
                continue;
            gh.box.Type = Box.BoxType.Anomaly;
            gh.SetupBox();

            Selection.activeTransform = gh.transform;
            CubeHex cubeHex = HexUtils.AxialToCubeDirect(gh.hex);
            List<CubeHex> inRange = Range(cubeHex, 3);
            foreach (CubeHex hex in inRange)
            {
                AxialHex axialHex = HexUtils.CubeToAxialDirect(hex);
                table.Remove(axialHex.GetHashCode());
            }
        }
    }

    private static void GenerateBlackHoles(UniverseInfo info)
    {
        Hashtable table = info.Grid.Clone() as Hashtable;
        int i = 0;
        while (table.Count > 0)
        {
            int rand = Random.Range(0, table.Count);
            HexGrid.GameHex[] arr = table.Values.Cast<HexGrid.GameHex>().ToArray();
            HexGrid.GameHex gh = arr[rand];
            i++;

            if (i > 50)
                break;

            if (gh.box.Type != Box.BoxType.Space)
                continue;
            gh.box.Type = Box.BoxType.BlackHole;
            gh.SetupBox();

            Selection.activeTransform = gh.transform;
            CubeHex cubeHex = HexUtils.AxialToCubeDirect(gh.hex);
            List<CubeHex> inRange = Range(cubeHex, 8);
            foreach (CubeHex hex in inRange)
            {
                AxialHex axialHex = HexUtils.CubeToAxialDirect(hex);
                table.Remove(axialHex.GetHashCode());
            }
        }
    }
}