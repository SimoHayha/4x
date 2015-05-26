using UnityEngine;
using System.Collections;
using System;

public abstract class Hex
{
    //[SerializeField]
    //private int[] cube;
    //[SerializeField]
    //private int[] axial;

    public abstract int Q(); //x
    public abstract int R(); //y
    public abstract int S(); //z

    public override int GetHashCode()
    {
        return Q() & (int)0xFFFF | R() << 16;
    }
}
