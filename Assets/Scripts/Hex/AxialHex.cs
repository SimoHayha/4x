using UnityEngine;
using System.Collections;

public class AxialHex : Hex
{
    private int q;
    private int r;

    public AxialHex(int q_, int r_) : base()
    {
        q = q_;
        r = r_;
    }

    public AxialHex(int q_, int r_, int s_) : base()
    {
        q = q_;
        r = r_;
    }

    public override int Q()
    {
        return q;
    }

    public override int R()
    {
        return r;
    }

    public override int S()
    {
        return -q - r;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
