using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    static void GenerateStars(UniverseInfo info)
    {

    }

    public static void Generate(UniverseInfo info)
    {
        foreach (DictionaryEntry e in info.Grid)
        {
            HexGrid.GameHex gameHex = e.Value as HexGrid.GameHex;
            gameHex.box = new Box();
            GameObject.Destroy(gameHex.boxMesh);
        }

        for (int i = 0; i < info.SystemCount; ++i)
        {
            int q = Random.Range(-info.Size, info.Size);
            int r1 = Mathf.Max(-info.Size, -q - info.Size);
            int r2 = Mathf.Min(info.Size, -q + info.Size);
            int r = Random.Range(r1, r2);

            AxialHex hex = new AxialHex(q, r);
            Debug.Log(q + " " + r);
            var gameHex = info.Grid[hex.GetHashCode()] as HexGrid.GameHex;
            gameHex.box = new Box();
            gameHex.box.Type = Box.BoxType.Star;
            gameHex.SetupBox();
        }

        for (int i = 0; i < info.BlackHoleCount; ++i)
        {
            int q = Random.Range(-info.Size, info.Size);
            int r1 = Mathf.Max(-info.Size, -q - info.Size);
            int r2 = Mathf.Min(info.Size, -q + info.Size);
            int r = Random.Range(r1, r2);

            AxialHex hex = new AxialHex(q, r);
            var gameHex = info.Grid[hex.GetHashCode()] as HexGrid.GameHex;
            gameHex.box = new Box();
            gameHex.box.Type = Box.BoxType.BlackHole;
            gameHex.SetupBox();
        }

        for (int i = 0; i < info.AnomalyCount; ++i)
        {
            int q = Random.Range(-info.Size, info.Size);
            int r1 = Mathf.Max(-info.Size, -q - info.Size);
            int r2 = Mathf.Min(info.Size, -q + info.Size);
            int r = Random.Range(r1, r2);

            AxialHex hex = new AxialHex(q, r);
            var gameHex = info.Grid[hex.GetHashCode()] as HexGrid.GameHex;
            gameHex.box = new Box();
            gameHex.box.Type = Box.BoxType.Anomaly;
            gameHex.SetupBox();
        }
    }
}