using UnityEngine;
using System.Collections;
using System;

public abstract class Hex
{
    //protected int q;
    //protected int r;
    //protected int s;

    private int[] cube;
    private int[] axial;

    // axial ctor
    //public Hex(int q, int r) 
    //{
    //    cube = new int[3];
    //    axial = new int[2];

    //    cube[0] = q;
    //    cube[1] = r;
    //    cube[2] = -q - r;
    //    axial[0] = q;
    //    axial[1] = r;

    //    if (Q() + R() + S() != 0)
    //        throw new Exception();
    //}

    // cube ctor
    //public Hex(int q, int r, int s)
    //{
    //    cube = new int[3];
    //    axial = new int[2];

    //    cube[0] = q;
    //    cube[1] = r;
    //    cube[2] = s;
    //    axial[0] = q;
    //    axial[1] = r;

    //    if (Q() + R() + S() != 0)
    //        throw new Exception();
    //}

    public abstract int Q(); //x
    public abstract int R(); //y
    public abstract int S(); //z

    //public override int GetHashCode()
    //{
    //    return (R().ToString() + Q().ToString() + S().ToString()).GetHashCode();
    //}

    //public Vector2 GetAxialCoord()
    //{
    //    Vector2 coord = new Vector2();
    //    coord.x = cube[0];
    //    coord.y = cube[2];
    //    return coord;
    //}

    //public Vector3 GetCubeCoord()
    //{
    //    Vector3 coord = new Vector3();
    //    coord.x = axial[0];
    //    coord.z = axial[1];
    //    coord.y = -coord.x - coord.z;
    //    return coord;
    //}
}
