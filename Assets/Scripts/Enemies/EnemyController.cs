using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Properties")]
    public Enemy Enemy;

    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _spriteRenderer.sprite = Enemy.Sprite;
    }
}
