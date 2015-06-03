using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float MoveSpeed = 10.0f;
    public float ZoomSpeed = 50.0f;

    void Update()
    {
        Vector3 moveOffset = Vector3.zero;
        Vector3 zoomOffset = Vector3.zero;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveOffset.x = MoveSpeed * horizontal;
        moveOffset.y = MoveSpeed * vertical;

        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
            zoomOffset.z += ZoomSpeed * Time.deltaTime;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            zoomOffset.z -= ZoomSpeed * Time.deltaTime;

        transform.Translate(moveOffset, Space.World);
        transform.Translate(zoomOffset, Space.Self);
    }
}
