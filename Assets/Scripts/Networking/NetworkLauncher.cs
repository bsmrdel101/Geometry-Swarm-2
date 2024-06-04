using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    [Header("References")]
    [SerializeField] private TMP_InputField _playerNameInput;
    [SerializeField] private Button _playBtn;


    private void Start()
    {
        _playerNameInput.text = PlayerPrefs.GetString("PlayerName");
    }

    public void OnChange_SetNickname()
    {
        if (_playerNameInput.text.Trim().Length > 0)
        {
            _playBtn.interactable = true;
            PlayerPrefs.SetString("PlayerName", _playerNameInput.text);
            PhotonNetwork.NickName = _playerNameInput.text;
        } else {
            _playBtn.interactable = false;
        }
    }

    public void OnClick_Connect()
    {
        if (!PhotonNetwork.IsConnected && _playerNameInput.text.Trim().Length > 0)
        {
            _playBtn.interactable = false;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}
