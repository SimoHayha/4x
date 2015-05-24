using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class HexUtils
{
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
}
