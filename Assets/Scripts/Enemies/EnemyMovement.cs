using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private NavMeshAgent _agent;
    private Transform _target;
    private bool _canMove = true;

    [Header("References")]
    [SerializeField] private EnemyController _controller;
    private Enemy _enemy;


    private void Start()
    {
        _enemy = _controller.Enemy;
        _agent.speed = _enemy.MoveSpeed;
        _agent.acceleration = _enemy.Acceleration;
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
        _agent.stoppingDistance = _enemy.FollowDistance;
        StartCoroutine(UpdateTarget());
    }

    public void SetTarget(Transform transform)
    {
        _target = transform;
    }

    private IEnumerator UpdateTarget()
    {
        while (_canMove)
        {
            yield return new WaitForSeconds(0.2f);
            FindClosetPlayer();
            if (!_target) continue;
            // TODO: Raycast to see if a wall is in the way of shooting

            _agent.SetDestination(_target.position);
        }
        _agent.SetDestination(transform.position);
        yield return new WaitForSeconds(0.2f);
    }

    private void FindClosetPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = 10000f;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                _target = player.transform;
            }
        }
    }
}
