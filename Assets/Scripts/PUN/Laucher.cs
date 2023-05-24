using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
namespace Mygame

{
    public class Laucher : MonoBehaviourPunCallbacks
    {
        #region private serielaizable Fields
        [Tooltip("The Max people who can join a room before a new one is created")]
        [SerializeField] private byte maxPlayersPerRoom = 4;
        [SerializeField] private GameObject controlPanel;
        [SerializeField] private GameObject progressLabel;
        #endregion
        #region Privite Fields
        private string gameVersion = "0.1";
        private bool isconnecting;
        #endregion
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        // Start is called before the first frame update
        void Start()
        {
            controlPanel.SetActive(true);
            progressLabel.SetActive(false);
        }

        #region public methods
        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
               isconnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
            controlPanel.SetActive(false);
            progressLabel.SetActive(true);
        }
        public void LoadArena()
        {
            PhotonNetwork.LoadLevel(1);
        }
        #endregion
        #region Call Backs
        public override void OnConnectedToMaster()
        {
            if (isconnecting)
            {
                Debug.Log("I have Connected");
                PhotonNetwork.JoinRandomRoom();
                isconnecting = false;
            }
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("I have disconnected");
            controlPanel.SetActive(true);
            progressLabel.SetActive(false);
            isconnecting = false;
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to Join a random room ");

            var room = new RoomOptions();

            room.MaxPlayers = maxPlayersPerRoom;

            PhotonNetwork.CreateRoom(null, new RoomOptions());
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room");
            //LoadArena();
        }

        #endregion 
    }
}
