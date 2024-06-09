using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyController _controller;
    [SerializeField] private BoxCollider2D _boxCol;
    [SerializeField] private PolygonCollider2D _polyCol;
    private Enemy _enemy;


    private void Start()
    {
        _enemy = _controller.Enemy;
        SetHitbox();
    }

    private void SetHitbox()
    {
        if (_enemy.HitboxType == HitboxType.Box) {
            _boxCol.enabled = true;
            _boxCol.size = _enemy.BoxCol;
        }
        if (_enemy.HitboxType == HitboxType.Poly) _polyCol.enabled = true;
    }
}
