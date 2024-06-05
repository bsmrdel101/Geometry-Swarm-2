using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class DebugManager : MonoBehaviourPunCallbacks
{
    [Header("Multiplayer")]
    [SerializeField] private bool _autoJoinRoom = false;


    private void Awake()
    {
        if (!_autoJoinRoom || PhotonNetwork.IsConnected) return;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Test", new RoomOptions{ MaxPlayers = 6, IsVisible = true, IsOpen = true, BroadcastPropsChangeToAll = true }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Area 1");
    }
}
