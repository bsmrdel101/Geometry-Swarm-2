using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _cursorObj;


    private void Start()
    {
        if (!PhotonNetwork.InRoom) return;
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        Cursor.visible = false;
        _cursorObj.gameObject.SetActive(true);
    }

    private void Update()
    {
        _cursorObj.transform.position = Input.mousePosition;
    }
}
