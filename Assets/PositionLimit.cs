using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLimit : MonoBehaviour
{
    public Transform limit;
     Vector3 StartPositon;
    // Start is called before the first frame update
    void Start()
    {
        StartPositon = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < limit.position.y)
        {
            Debug.Log("y psoition" + StartPositon.y);
            transform.position = StartPositon;
        }
    }
}
