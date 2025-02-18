using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // If no camera is assigned, use the main camera
        }
    }

    void Update()
    {
        // Make the canvas face the camera
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);
    }
}
