using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float Speed = 50.0f;

    void Update()
    {
        Vector3 offset = Vector3.zero;

        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            offset.z += Speed * Time.deltaTime;
            //Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - 1, 6);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            offset.z -= Speed * Time.deltaTime;

        transform.position += offset;
    }
}
