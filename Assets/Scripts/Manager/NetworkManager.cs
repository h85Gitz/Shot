using System;
using Photon.Pun;
using Photon.Realtime;
using UI;
using UnityEngine;

namespace Manager
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public GameObject playerPrefab;
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
            UIEffect.OnUIEffected += OnJoinedLobby;
        }

        public override void OnConnectedToMaster()
        {
           PhotonNetwork.JoinLobby();
           Debug.Log("Connected to Photon Master Server.");
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby.");
        }

        public void CreateRoom()
        {
            RoomOptions roomOptions = new()
            {
                MaxPlayers = 2
            };
            PhotonNetwork.CreateRoom("ShootGame", roomOptions);
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom("ShootGame");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined the room.");
            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        }

    }
}