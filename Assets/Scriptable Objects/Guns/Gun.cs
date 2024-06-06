using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Assets/Gun")]
public class Gun : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public float BulletSpeed = 6f;
    public float FireDelay = 0.4f;
    public float Damage = 1f;
    public float BulletSpread = 0f;
}
