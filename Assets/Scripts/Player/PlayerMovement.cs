using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 6f;

    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;


    private void Start()
    {
        if (!photonView.IsMine) enabled = false;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical"); 
        Vector3 movement = new Vector3(x, y, 0) * Time.deltaTime;
        _rb.MovePosition(transform.position + movement * _moveSpeed);
    }
}
