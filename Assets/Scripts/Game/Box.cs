using UnityEngine;
using System.Collections;

public class Box
{
    public enum BoxType
    {
        Space,
        Planet,
        AsteroidBelt,
        BlackHole,
        Anomaly,
        Star
    }

    public BoxType Type;
}