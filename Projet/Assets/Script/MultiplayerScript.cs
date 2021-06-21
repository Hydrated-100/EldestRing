using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
public class MultiplayerScript : MonoBehaviourPunCallbacks
{
        [SerializeField] private GameObject findOpponent;
        [SerializeField] private GameObject waitingStatus ; //scène avec waiting text
        [SerializeField] private TextMeshProUGUI waitingText;
        
        private bool isConnect = false;

        private const String Ver = "6.";
        private const int MaxPlayer = 3;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void FindOpponent() //associer au bouton multi du menu
        {
            isConnect = true;

            findOpponent.SetActive(false);
            waitingStatus.SetActive(true);

            waitingText.text = "Wait...";

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = Ver;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            if (isConnect)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            waitingStatus.SetActive(false);
            findOpponent.SetActive(true);
            Debug.Log($"Disconected for {cause}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Creating room");
            PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = MaxPlayer});
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Entered");
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            Debug.Log(playerCount);

            if (playerCount != MaxPlayer)
            {
                waitingText.text = "More waiting";
            }
            else
            {
                waitingText.text = "Check make Atheist";
                PhotonNetwork.LoadLevel("NiveauMulti");
            }
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayer)
            {
                Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
                PhotonNetwork.CurrentRoom.IsOpen = false;
                waitingText.text = "Check make Atheist";

                PhotonNetwork.LoadLevel("NiveauMulti");
                
            }

        }
}
