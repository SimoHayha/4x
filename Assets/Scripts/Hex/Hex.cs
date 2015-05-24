using UnityEngine;
using System.Collections;
using System;

public struct Hex
{
    private int[] v;

    public Hex(int q, int r)
    {
        v = new int[3];

        v[0] = q;
        v[1] = r;
        v[2] = -q - r;

        if (Q() + R() + S() != 0)
            throw new Exception();
    }

    public Hex(int q, int r, int s)
    {
        v = new int[3];

        v[0] = q;
        v[1] = r;
        v[2] = s;

        if (Q() + R() + S() != 0)
            throw new Exception();
    }

    public int Q()
    {
        return v[0];
    }

    public int R()
    {
        return v[1];
    }

    public int S()
    {
        return v[2];
    }
}
