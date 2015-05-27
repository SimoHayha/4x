using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class HexUtils
{
    private static AxialHex[] directions = { new AxialHex(1, 0), new AxialHex(1, -1), new AxialHex(0, -1),
                                            new AxialHex(-1, 0), new AxialHex(-1, 1), new AxialHex(0, 1) };

    public static CubeHex CubeAdd(CubeHex a, CubeHex b)
    {
        return new CubeHex(a.Q() + b.Q(), a.R() + b.R(), a.S() + b.S());
    }

    public static AxialHex AxialAdd(AxialHex a, AxialHex b)
    {
        return new AxialHex(a.Q() + b.Q(), a.R() + b.R());
    }

    public static CubeHex CubeSub(CubeHex a, CubeHex b)
    {
        return new CubeHex(a.Q() - b.Q(), a.R() - b.R(), a.S() - b.S());
    }

    public static AxialHex AxialSub(AxialHex a, AxialHex b)
    {
        return new AxialHex(a.Q() - b.Q(), a.R() - b.R());
    }

    public static AxialHex HexDirection(int direction)
    {
        return directions[direction];
    }

    public static AxialHex HexNeighbor(AxialHex hex, int direction)
    {
        AxialHex dir = HexDirection(direction);
        return new AxialHex(hex.Q() + dir.Q(), hex.R() + dir.R());
    }

    public static Vector3 HexToPixel(Layout layout, Hex h)
    {
        Orientation M = layout.orientation;
        float x = (M.f0 * h.Q() + M.f1 * h.R()) * layout.size.x;
        float y = (M.f2 * h.Q() + M.f3 * h.R()) * layout.size.y;
        return new Vector3(x + layout.origin.x, y + layout.origin.y);
    }

    public static Vector3 HexCornerOffset(Layout layout, int corner)
    {
        Vector3 size = layout.size;
        float angle = 2.0f * Mathf.PI * (corner + layout.orientation.startAngle) / 6.0f;
        return new Vector3(size.x * Mathf.Cos(angle), size.y * Mathf.Sin(angle));
    }

    public static List<Vector3> PolygonCorner(Layout layout, Hex h)
    {
        List<Vector3> corners = new List<Vector3>();
        Vector3 center = HexToPixel(layout, h);
        for (int i = 0; i < 6; ++i)
        {
            Vector3 offset = HexCornerOffset(layout, i);
            corners.Add(new Vector3(center.x + offset.x, center.y + offset.y));
        }
        return corners;
    }

    public static List<Vector2> PolygonCorner2d(Layout layout, Hex h)
    {
        List<Vector2> corners = new List<Vector2>();
        Vector3 center = HexToPixel(layout, h);
        for (int i = 0; i < 6; ++i)
        {
            Vector2 offset = HexCornerOffset(layout, i);
            corners.Add(new Vector3(center.x + offset.x, center.y + offset.y));
        }
        return corners;
    }

    public static Vector2 CubeToAxial(CubeHex h)
    {
        Vector2 v = new Vector2();
        v.x = h.Q();
        v.y = h.S();
        return v;
    }

    public static Vector3 AxialToCube(AxialHex h)
    {
        Vector3 v = new Vector3();
        v.x = h.Q();
        v.z = h.R();
        v.y = -v.x - v.z;
        return v;
    }

    public static AxialHex CubeToAxialDirect(CubeHex h)
    {
        Vector2 v = new Vector2();
        v.x = h.Q();
        v.y = h.S();
        return new AxialHex((int)v.x, (int)v.y);
    }

    public static CubeHex AxialToCubeDirect(AxialHex h)
    {
        Vector3 v = new Vector3();

        //v.x = -v.y - v.z;
        v.x = h.Q();
        v.z = h.R();
        v.y = -v.x - v.z;
        //return new CubeHex((int)v.y, (int)v.x, (int)v.z);
        return new CubeHex((int)v.x, (int)v.y, (int)v.z);
    }

    public static int CubeDistance(CubeHex a, CubeHex b)
    {
        return Mathf.Max(Mathf.Abs(a.Q() - b.Q()), Mathf.Abs(a.R() - b.R()), Mathf.Abs(a.S() - b.S()));
    }

    public static int AxialDistance(AxialHex a, AxialHex b)
    {
        Vector3 va = AxialToCube(a);
        Vector3 vb = AxialToCube(b);
        return CubeDistance(new CubeHex((int)va.x, (int)va.y, (int)va.z), new CubeHex((int)vb.x, (int)vb.y, (int)vb.z));
    }
}
