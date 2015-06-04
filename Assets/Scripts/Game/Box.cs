using UnityEngine;
using System.Collections;

[System.Serializable]
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

    public DataHandler Handler;
}