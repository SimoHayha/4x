using UnityEngine;
using System.Collections;
using UnityEditor;

[InitializeOnLoad]
public class MeshLoader
{
    public static Mesh[] StarMeshes;

    static MeshLoader()
    {
        StarMeshes = Resources.LoadAll<Mesh>("Meshes");
    }
}
