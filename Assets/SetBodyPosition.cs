using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBodyPosition : MonoBehaviour
{

    public Transform CameraPosition;
    public Vector3 offset;
    private void FixedUpdate()
    {
        transform.position = CameraPosition.position + offset;
    }
}
