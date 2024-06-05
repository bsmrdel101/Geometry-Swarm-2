using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Lobby UI")]
    [SerializeField] private GameObject _gameSetupPanel;
    [SerializeField] private GameObject _roomPanel;
    [SerializeField] private TMP_InputField _gameCodeInput;

    [Header("Room")]
    [SerializeField] private Transform _playerList;
    [SerializeField] private GameObject _playerElement;
    [SerializeField] private TMP_Text _roomName;

    [Header("References")]
    [SerializeField] private Button _startBtn;
    [SerializeField] private PhotonView _view;


    private void Start()
    {
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
        else
            _gameSetupPanel.SetActive(true);
    }

    public void OnClick_ExitMainMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Main Menu");
    }

    public void OnClick_StartGame()
    {
        _view.RPC("StartGame", RpcTarget.All);
    }

    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(GenerateCode(), new RoomOptions{ MaxPlayers = 6, IsVisible = true, IsOpen = true, BroadcastPropsChangeToAll = true });
    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(_gameCodeInput.text);
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void UpdatePlayerList()
    {
        ClearPlayerList();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject _playerElementObj = Instantiate(_playerElement, Vector3.zero, Quaternion.identity);
            _playerElementObj.transform.SetParent(_playerList);
            _playerElementObj.GetComponent<PlayerElement>().SetName(player.NickName);
        }

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            _startBtn.interactable = true;
        } else {
            _startBtn.interactable = false;
        }
    }

    private void ClearPlayerList()
    {
        foreach (Transform child in _playerList)
        {
            Destroy(child.gameObject);
        }
    }

    public string GenerateCode()
    {
        StringBuilder builder = new StringBuilder();
        Enumerable
        .Range(65, 26)
            .Select(e => ((char)e).ToString())
            .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            .OrderBy(e => Guid.NewGuid())
            .Take(5)
            .ToList().ForEach(e => builder.Append(e));
        return builder.ToString();
    }
    

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public override void OnJoinedLobby()
    {
        _gameSetupPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        _gameSetupPanel.SetActive(false);
        _roomPanel.SetActive(true);
        _roomName.text = $"{_roomName.text} {PhotonNetwork.CurrentRoom.Name}";
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }


    [PunRPC]
    private void StartGame()
    {
        PhotonNetwork.LoadLevel("Area 1");
    }
}
