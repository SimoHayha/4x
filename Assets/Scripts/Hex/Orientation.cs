using UnityEngine;
using System.Collections;

public struct Orientation
{
    public float f0;
    public float f1;
    public float f2;
    public float f3;
    public float b0;
    public float b1;
    public float b2;
    public float b3;
    public float startAngle;

    public Orientation(float f0_, float f1_, float f2_, float f3_,
                       float b0_, float b1_, float b2_, float b3_,
                       float startAngle_)
    {
        f0 = f0_;
        f1 = f1_;
        f2 = f2_;
        f3 = f3_;
        b0 = b0_;
        b1 = b1_;
        b2 = b2_;
        b3 = b3_;
        startAngle = startAngle_;
    }

    public static Orientation LayoutPointy()
    {
        return new Orientation(Mathf.Sqrt(3.0f), Mathf.Sqrt(3.0f) / 2.0f, 0.0f, 3.0f / 2.0f,
                               Mathf.Sqrt(3.0f) / 3.0f, -1.0f / 3.0f, 0.0f, 2.0f / 3.0f,
                               0.5f);
    }

    public static Orientation LayoutFlat()
    {
        return new Orientation(3.0f / 2.0f, 0.0f, Mathf.Sqrt(3.0f) / 2.0f, Mathf.Sqrt(3.0f),
                               2.0f / 3.0f, 0.0f, -1.0f / 3.0f, Mathf.Sqrt(3.0f) / 3.0f,
                               0.0f);
    }
}