using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCamera : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        // Canvas luôn quay mặt về camera
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }

}
