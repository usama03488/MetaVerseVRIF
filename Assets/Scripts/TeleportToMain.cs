using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToMain : MonoBehaviour
{
    public bool AllowTeleport;
    public GameObject Canvas1;
    public Transform OutsideLocation;
    public Transform Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
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
        if (AllowTeleport)
        {
            if (InputBridge.Instance.XButtonDown || Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Update11");
                Player.gameObject.SetActive(false);
                Player.transform.position = OutsideLocation.transform.position;
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
