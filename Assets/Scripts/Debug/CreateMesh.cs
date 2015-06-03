using UnityEngine;
using System.Collections;

public class CreateMesh : MonoBehaviour
{
    public HexMesh Mesh;

    void Start()
    {
        Orientation orientation = Orientation.LayoutPointy();
        //Orientation orientation = Orientation.LayoutFlat();
        Layout layout = new Layout(orientation, new Vector3(1.0f, 1.0f, 0.0f), Vector3.zero);

        Mesh.Create(layout);
    }

    void Update()
    {

    }
}
