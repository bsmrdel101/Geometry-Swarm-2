using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [Header("References")]
    [SerializeField] private GameObject _cam;


    private void Start()
    {
        if (!photonView.IsMine)
        {
            _cam.SetActive(false);
            enabled = false;
        }
    }
}
