using UnityEngine;
using System.Collections;
using UnityEditor;

[InitializeOnLoad]
public class PrefabLoader
{
    public static Hashtable Prefabs;

    static PrefabLoader()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");

        Prefabs = new Hashtable();
        foreach (GameObject prefab in prefabs)
            Prefabs.Add(prefab.name.ToLower().GetHashCode(), prefab);
    }
}
