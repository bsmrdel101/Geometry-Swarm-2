using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Assets/Gun")]
public class Gun : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public AudioClip ShotSfx;

    [Header("Gun Properties")]
    public float BulletSpeed = 6f;
    public float ReloadTime = 3f;
    public float Damage = 1f;
    public int MagSize = 6;
    public float BulletSpread = 0f;
    public bool IsAuto = false;

    [Tooltip("Cooldown before shooting another bullet")]
    public float FireDelay = 0.2f;
    [Tooltip("Amount of distance gun sprite is held away from player")]
    public float Offset = 0.8f;
}
