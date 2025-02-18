
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

namespace BNG {
    public class NetworkManager :

        MonoBehaviourPunCallbacks


    {

        /// <summary>
        /// Maximum number of players per room. If the room is full, a new radom one will be created.
        /// </summary>
        [Tooltip("Maximum number of players per room. If the room is full, a new random one will be created. 0 = No Max.")]
        [SerializeField]
        private byte maxPlayersPerRoom = 0;

        [Tooltip("If true, the JoinRoomName will try to be Joined On Start. If false, need to call JoinRoom yourself.")]
        public bool JoinRoomOnStart = true;

        [Tooltip("If true, do not destroy this object when moving to another scene")]
        public bool dontDestroyOnLoad = true;

        public string JoinRoomName = "RandomRoom";

        [Tooltip("Game Version can be used to separate rooms.")]
        public string GameVersion = "1";

        [Tooltip("Name of the Player object to spawn. Must be in a /Resources folder.")]
        public string RemotePlayerObjectName = "RemotePlayer";

        [Tooltip("Optional GUI Text element to output debug information.")]
        public Text DebugText;

        ScreenFader sf;


        public GameObject PlayerController;
        public
            Transform SpawnPlayer;


        public GameObject Dectector0;
        public GameObject Dectector1;
        public GameObject Dectector2;
        public GameObject Dectector3;

        public Transform DectectorParentMachine1;
        public Transform DectectorParentMachine2;

        public Transform DectectorParentMachine2_ticTac;
        public Transform DectectorParentMachine3_SpaceShooter;


        void Awake() {

            // Required if you want to call PhotonNetwork.LoadLevel() 
            PhotonNetwork.AutomaticallySyncScene = true;

            if (dontDestroyOnLoad) {
                DontDestroyOnLoad(this.gameObject);
            }

            if(Camera.main != null) {
                sf = Camera.main.GetComponentInChildren<ScreenFader>(true);
            }
        }

        void Start() {
            /*   // Connect to Random Room if Connected to Photon Server
               if (PhotonNetwork.IsConnected) {
                   if (JoinRoomOnStart) {
                       LogText("Joining Room : " + JoinRoomName);
                       PhotonNetwork.JoinRoom(JoinRoomName);
                   }
               }
               // Otherwise establish a new connection. We can then connect via OnConnectedToMaster
               else {
                   PhotonNetwork.ConnectUsingSettings();
                   PhotonNetwork.GameVersion = GameVersion;
               }*/
                PhotonNetwork.ConnectUsingSettings();
        }

        void Update() {
            // Show Loading Progress
            if (PhotonNetwork.LevelLoadingProgress > 0 && PhotonNetwork.LevelLoadingProgress < 1) {
                Debug.Log(PhotonNetwork.LevelLoadingProgress);
            }
        }

        public void TestRoom()
        {
            PhotonNetwork.JoinOrCreateRoom("MyTestingRoom", new RoomOptions { MaxPlayers = maxPlayersPerRoom }, TypedLobby.Default);
        }

        public void CustomConectToServer()
        {
            if (PhotonNetwork.IsConnected)
            {
                if (JoinRoomOnStart)
                {
                    // LogText("Joining Room : " + JoinRoomName);
                    PhotonNetwork.JoinRoom(JoinRoomName);
                }
            }
            // Otherwise establish a new connection. We can then connect via OnConnectedToMaster
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = GameVersion;
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message) {
            LogText("Room does not exist. Creating <color=yellow>" + JoinRoomName + "</color>");
            PhotonNetwork.CreateRoom(JoinRoomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, TypedLobby.Default);
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            Debug.Log("OnJoinRandomFailed Failed, Error : " + message);
        }

        public override void OnConnectedToMaster() {

            LogText("Connected to Master Server. \n");
            PhotonNetwork.JoinLobby();
         /*   if (JoinRoomOnStart) {
                LogText("Joining Room : <color=aqua>" + JoinRoomName + "</color>");
                PhotonNetwork.JoinRoom(JoinRoomName);
            }*/
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            float playerCount = PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null ? PhotonNetwork.CurrentRoom.PlayerCount : 0;

            LogText("Connected players : " + playerCount);
        }

        public override void OnJoinedRoom() {

          if(PhotonNetwork.IsMasterClient)
            {
              /*  GameObject Temp1 = PhotonNetwork.Instantiate(Dectector0.name, DectectorParentMachine1.transform.position,Quaternion.identity);
                GameObject Temp2 = PhotonNetwork.Instantiate(Dectector1.name, DectectorParentMachine2.transform.position,Quaternion.identity);
                GameObject Temp3 = PhotonNetwork.Instantiate(Dectector2.name, DectectorParentMachine2_ticTac.transform.position,Quaternion.identity);
                GameObject Temp4 = PhotonNetwork.Instantiate(Dectector3.name, DectectorParentMachine3_SpaceShooter.transform.position,Quaternion.identity);
            
       

                Temp1.transform.parent = DectectorParentMachine1.transform;
                Temp2.transform.parent = DectectorParentMachine2.transform;
                Temp3.transform.parent = DectectorParentMachine2_ticTac.transform;
                Temp4.transform.parent = DectectorParentMachine3_SpaceShooter.transform;
             
        


                Temp1.transform.localPosition = new Vector3();
                Temp2.transform.localPosition = new Vector3();
                Temp3.transform.localPosition = new Vector3();
                Temp4.transform.localPosition = new Vector3();
            
         

                Temp1.GetComponent<Detector>().Game = FindObjectOfType<GameReferenceHolder>().FruitGame;
                Temp2.GetComponent<Detector>().Game = FindObjectOfType<GameReferenceHolder>().WhacAMole1;
                Temp3.GetComponent<Detector>().Game = FindObjectOfType<GameReferenceHolder>().TickTacToe;
                Temp4.GetComponent<Detector>().Game = FindObjectOfType<GameReferenceHolder>().SpaceShooter;
               */

            }

            LogText("Joined Room. Creating Remote Player Representation.");

            // Network Instantiate the object used to represent our player. This will have a View on it and represent the player         
            GameObject player = PhotonNetwork.Instantiate(RemotePlayerObjectName, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            NetworkPlayer np = player.GetComponent<NetworkPlayer>();
            if (np) {
                np.transform.name = "MyRemotePlayer";
                np.AssignPlayerObjects();
               // FindObjectOfType<TeleportToRooms>().Player = np.transform;
            }
            PlayerController.transform.position = SpawnPlayer.transform.position;

            
        }

        public override void OnDisconnected(DisconnectCause cause) {
            LogText("Disconnected from PUN due to cause : " + cause);

            if (!PhotonNetwork.ReconnectAndRejoin()) {
                LogText("Reconnect and Joined.");
            }

            base.OnDisconnected(cause);
           // cachedRoomList.Clear();

        }

        public void LoadScene(string sceneName) {
            // Fade Screen out
            StartCoroutine(doLoadLevelWithFade(sceneName));
        }

        IEnumerator doLoadLevelWithFade(string sceneName) {

            if (sf) {
                sf.DoFadeIn();
                yield return new WaitForSeconds(sf.SceneFadeInDelay);
            }

            PhotonNetwork.LoadLevel(sceneName);

            yield return null;
        }

        void LogText(string message) {

            // Output to worldspace to help with debugging.
            if (DebugText) {
                DebugText.text += "\n" + message;
            }

            Debug.Log(message);
        }


        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                // When the application is paused (minimized or in background)
            //    Debug.LogError("App is paused");
                if (PhotonNetwork.IsConnected)
                {
                    // Optionally stop or handle Photon network state during pause

                }
            }
            else
            {
                // When the application is resumed (foreground)
           //     Debug.LogError("App is resumed");
                if (!PhotonNetwork.IsConnected)
                {
                    // Optionally reconnect if needed
                    PhotonNetwork.Reconnect();
                }
            }
        }

        // Called when the application gains or loses focus
        void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                // When the application gains focus
                Debug.LogError("App gained focus");
                if (!PhotonNetwork.IsConnected)
                {
                    // Attempt to reconnect when focus is regained
                    PhotonNetwork.Reconnect();
                }
            }
            else
            {
                // When the application loses focus
                Debug.LogError("App lost focus");
                // You can handle disconnection, pausing logic, etc.
            }
        }


        #region RoomList
        private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
        public GameObject PrefabContent;
        public Transform Content;

        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
        
            for (int i = 0; i < roomList.Count; i++)
            {
                RoomInfo info = roomList[i];
                if (info.RemovedFromList)
                {
                    Debug.Log("SomeOneCreate Rooms Remove"); 
                    cachedRoomList.Remove(info.Name);
                    foreach (var Obj in Content.GetComponentsInChildren<Transform>())
                    {
                        if (Obj.GetComponent<RoomDetails>())
                        {
                            if (Obj.GetComponent<RoomDetails>().roomName.text == info.Name)
                            {
                                Destroy(Obj.gameObject);
                            
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("SomeOneCreate Rooms Added");

                    foreach (var Obj in Content.GetComponentsInChildren<Transform>())
                    {
                        if (Obj.GetComponent<RoomDetails>())
                        {
                            if (Obj.GetComponent<RoomDetails>().roomName.text == info.Name)
                            {
                                Destroy(Obj.GetComponent<RoomDetails>().gameObject);
                            }
                        }
                    }
                    cachedRoomList[info.Name] = info;
                    GameObject Room = Instantiate(PrefabContent, Content);
                    Room.GetComponent<RoomDetails>().roomName.text = info.Name;
                    Room.GetComponent<RoomDetails>().TotalJoinPlayer.text = info.PlayerCount.ToString();



                }
            }
        }

        public GameObject MenuScreen;
        public override void OnJoinedLobby()
        {
            MenuScreen.SetActive(true);
            // cachedRoomList.Clear();
            Debug.Log("Lobby Joined");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("SomeOneCreate Rooms");
            UpdateCachedRoomList(roomList);
        }

        public override void OnLeftLobby()
        {
           // cachedRoomList.Clear();
        }

        #endregion
    }
}

