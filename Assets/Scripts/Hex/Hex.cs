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

    // hash(x,y,z) = ( x p1 xor y p2 xor z p3) mod n
    public override int GetHashCode()
    {
        //return (Q() * 73856093 ^ R() * 19349663 ^ S() ^ 83492791) % 50;
        return Q() & (int)0xFFFF | R() << 16;
    }
}
