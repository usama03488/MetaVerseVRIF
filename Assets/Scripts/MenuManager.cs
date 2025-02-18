using BNG;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] EmojiModel;
    public GameObject MenuContent;
    public NetworkManager Networking;

    public GameObject LoadingScreen;
    
    #region Singleton
    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Characters Functions
    public void SelectCharacter(int index)
    {
        foreach (var model in EmojiModel)
        {
            model.SetActive(false);
        }
        EmojiModel[index].SetActive(true);

        PlayerPrefs.SetInt("Character", index);

    }
    public void SaveCharacterName(string name)
    {
        PlayerPrefs.SetString("CharacterName", name);
        string Index = PlayerPrefs.GetString("CharacterName");
        Networking.RemotePlayerObjectName = Index;
    }

    #endregion

    public void EnterMetaverse()
    {

        Networking.JoinRoomOnStart = true;
        Networking.CustomConectToServer();
        MenuContent.SetActive(false);
    }


    #region QuitGame
    public void QuitGame()
    {
        Application.Quit();

    }
    #endregion

    #region CreateRoom
    public TMP_InputField CreateRoomInputField;
    public int maxPlayersPerRoom;
    public void CreateRoom()
    {
        LoadingScreen.SetActive(true);
        PhotonNetwork.CreateRoom(CreateRoomInputField.text, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, TypedLobby.Default);
    }
    #endregion

    public TMP_InputField NameText;
    public GameObject Menu;
    #region Functions
    public void SaveName()
    {
        PhotonNetwork.NickName = NameText.text;
        Menu.SetActive(true);
    }
    #endregion



}
