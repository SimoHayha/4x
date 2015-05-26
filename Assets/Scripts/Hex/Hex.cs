using UnityEngine;
using System.Collections;
using System;

public abstract class Hex
{
    private int[] cube;
    private int[] axial;

    public abstract int Q(); //x
    public abstract int R(); //y
    public abstract int S(); //z

    public override int GetHashCode()
    {
        return (S().ToString() + R().ToString() + Q().ToString()).GetHashCode();
    }
}
