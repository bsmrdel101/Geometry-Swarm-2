using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAI
{
    Melee,
    Ranged
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Assets/Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Enemy Properties")]
    public string Name;
    public Sprite Sprite;
    public EnemyAI EnemyAI;
    public Vector2[] Colision;

    [Header("Enemy Stats")]
    public int MaxHp;
    public float MoveSpeed;
}
