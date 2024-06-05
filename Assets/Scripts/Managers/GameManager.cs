using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        if (!PhotonNetwork.InRoom) return;
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        Cursor.visible = false;
    }
}
