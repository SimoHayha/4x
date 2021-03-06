﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class CubeHex : Hex
{
    [SerializeField]
    private int q;
    [SerializeField]
    private int r;
    [SerializeField]
    private int s;

    public CubeHex(int q_, int r_, int s_) : base()
    {
        q = q_;
        r = r_;
        s = s_;
    }

    public CubeHex(int q_, int r_) : base()
    {
        q = q_;
        r = q_;
        s = -q_ - r_;
    }

    // x
    public override int Q()
    {
        return q;
    }

    // y
    public override int R()
    {
        return r;
    }

    // z
    public override int S()
    {
        return s;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return "c(" + q + "," + r + "," + s + ")";
    }
}
