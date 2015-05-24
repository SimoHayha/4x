using UnityEngine;
using System.Collections;

public struct Layout
{
    public Orientation orientation;
    public Vector3 size;
    public Vector3 origin;

    public Layout(Orientation orientation_, Vector3 size_, Vector3 origin_)
    {
        orientation = orientation_;
        size = size_;
        origin = origin_;
    }
}