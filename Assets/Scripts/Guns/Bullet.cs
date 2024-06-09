using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Properties")]
    [HideInInspector] public Vector2 Velocity;


    private void Update()
    {
        transform.position += new Vector3(Velocity.x, Velocity.y) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "Bullet" && other.tag != "EnemyBullet")
        {
            Destroy(this.gameObject);
        }
    }
}
