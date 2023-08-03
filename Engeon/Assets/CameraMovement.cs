using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;
    void Start()
    {
        camera = Camera.main;
        camera.orthographicSize = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("mouse X: " + -Input.GetAxis("Mouse X"));
            Debug.Log("mouse Y: " + -Input.GetAxis("Mouse Y"));

            Vector3 drag = new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0);
            drag.x /= camera.orthographicSize * camera.aspect;
            drag.y /= camera.orthographicSize;
            transform.Translate(drag);
        }
        if (camera.orthographicSize + Input.mouseScrollDelta.y >= 1)
        {
            camera.orthographicSize += Input.mouseScrollDelta.y;
        }

    }
}
