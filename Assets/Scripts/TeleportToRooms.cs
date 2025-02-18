using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToRooms : MonoBehaviour
{

    public bool AllowTeleport;
    public GameObject Canvas1;
    public Transform InsideLocation;
    public Transform Player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Player")
        {
            AllowTeleport = true;
            Canvas1.SetActive(true);
        
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            AllowTeleport = false;
            Canvas1.SetActive(false);
        
        }
    }

    private void Update()
    {
       // Player.transform.localPosition = new Vector3(10, 0, 0);
        if (AllowTeleport)
        {
            if (InputBridge.Instance.XButtonDown || Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Update11");
                Player.gameObject.SetActive(false);
                Player.transform.position =  InsideLocation.transform.position;
                Invoke(nameof(RestorePlayer), 0.2f);
                AllowTeleport = false;
                Canvas1.SetActive(false);
            }
        }
    }
    void RestorePlayer()
    {
        Player.gameObject.SetActive(true);
    }

}
