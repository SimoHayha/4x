using UnityEngine;
using System.Collections;

public class InfiniteRotation : MonoBehaviour
{
    public Vector3 Angles;
    public float Force;

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(Angles * Force * Time.deltaTime);
    }
}