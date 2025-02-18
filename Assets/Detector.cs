using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Status _Status;
    public PhotonView View;
    public GameObject Game;
    public string PlayerOccupiedId;
    public int assignGameNumber;
    private void Start()
    {
        //  Game = FindObjectOfType<GameReferenceHolder>().TickTacToe1;
        if (assignGameNumber == 0)
        {
           Game = FindObjectOfType<GameReferenceHolder>().FruitGame;
        }
        else if (assignGameNumber == 1)
        {
            Game = FindObjectOfType<GameReferenceHolder>().WhacAMole1;
        }
        else if (assignGameNumber == 2)
        {

            Game = FindObjectOfType<GameReferenceHolder>().CarChase;
        }
        else if (assignGameNumber == 3)
        {
            Game = FindObjectOfType<GameReferenceHolder>().SpaceShooter;

        }
        else if (assignGameNumber == 4)
        {
           // Game = FindObjectOfType<GameReferenceHolder>().WhacAMole2;

        }
        else if (assignGameNumber == 5)
        {
         //   Game = FindObjectOfType<GameReferenceHolder>().WhacAMole3;

        }

    }

    private void OnTriggerEnter(Collider other)
    {


        /*    if (_Status == Status.Available)
            {
                if (other.transform.tag == "Player")
                {
                View.RPC(nameof(RPC_OccupiedID), RpcTarget.AllBuffered, FindObjectOfType<CustomRemotePlayer>().RemotePlayerID);
                Game.SetActive(true);
                    View = PhotonView.Get(this);
                    View.RPC(nameof(RPC_DetectionOccupied), RpcTarget.AllBuffered);
                }
            }
            else
            {
                View.RPC(nameof(RPC_Message), RpcTarget.All, PlayerOccupiedId);
            }*/

        if (other.transform.tag == "Player")
        {
         //   View.RPC(nameof(RPC_OccupiedID), RpcTarget.AllBuffered, FindObjectOfType<CustomRemotePlayer>().RemotePlayerID);
            Game.SetActive(true);
        //    View = PhotonView.Get(this);
        ///    View.RPC(nameof(RPC_DetectionOccupied), RpcTarget.AllBuffered);
        }


    }
    private void OnTriggerExit(Collider other)
    {

        /*   if (FindObjectOfType<CustomRemotePlayer>().RemotePlayerID == PlayerOccupiedId)
           {


               if (other.transform.tag == "Player")
               {
                   Game.SetActive(false);
                   View = PhotonView.Get(this);
                   View.RPC(nameof(RPC_DetectionAvailable), RpcTarget.AllBuffered);
               }
           }*/
        if (other.transform.tag == "Player")
        {
            Game.SetActive(false);
         //  View = PhotonView.Get(this);
         //   View.RPC(nameof(RPC_DetectionAvailable), RpcTarget.AllBuffered);
        }

    }
    [PunRPC]
    public void RPC_OccupiedID(string ID)
    {
        PlayerOccupiedId = ID;
    }

    [PunRPC]
    public void RPC_Message(string ID)
    {
        Debug.Log("This Game is Capture by ID " + ID);
    }


    [PunRPC]
    public void RPC_DetectionOccupied()
    {
        _Status = Status.Occupied;
    }
    [PunRPC]
    public void RPC_DetectionAvailable()
    {
        _Status = Status.Available;
    }

}


public enum Status
{
    Available,
    Occupied
}
