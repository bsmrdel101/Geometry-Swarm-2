using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAI
{
    Melee,
    Ranged
}

public enum HitboxType
{
    Box,
    Poly
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Assets/Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Enemy Properties")]
    public string Name;
    public Sprite Sprite;
    public EnemyAI EnemyAI;

    [Header("Enemy Stats")]
    public int MaxHp;
    public float MoveSpeed;
    public float Acceleration = 8f;

    [Header("Hitbox")]
    public HitboxType HitboxType;
    [HideInInspector] public Vector2 BoxCol = new Vector2(0.8648649f, 0.8648649f);
    [HideInInspector] public Vector2[] PolyColPoints;
}
