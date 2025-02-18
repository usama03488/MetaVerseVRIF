
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomDetails : MonoBehaviour
{

    public TMP_Text roomName;
    public Button JoinBtn;
    public TMP_Text TotalJoinPlayer;
    private void Start()
    {
        JoinBtn.onClick.AddListener(JoinRoomBtn);
    }
    public void JoinRoomBtn()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }
}
