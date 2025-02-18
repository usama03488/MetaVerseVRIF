using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantChild : MonoBehaviour
{
    public Transform childObject;  // Assign the child object in the inspector
    private Quaternion initialLocalRotation;
    private Vector3 initialLocalPosition;

    void Start()
    {
        if (childObject != null)
        {
            // Store the initial local rotation and position of the child object
            initialLocalRotation = childObject.rotation;
            initialLocalPosition = childObject.position;
        }
    }

    void LateUpdate()
    {
        if (childObject != null)
        {
            // Adjust the child's local rotation to maintain the same visual orientation
            childObject.rotation = Quaternion.Inverse(transform.rotation) * initialLocalRotation;

            // Adjust the child's local position to maintain the same world position
            childObject.position = transform.InverseTransformPoint(childObject.position);
        }
    }
}
