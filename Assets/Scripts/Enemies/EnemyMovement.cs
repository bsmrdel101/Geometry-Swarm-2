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
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
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

            if (_enemy.EnemyAI == EnemyAI.Ranged)
            {
                float distance = Vector3.Distance(_target.position, transform.position);
                if (distance > 8)
                    _agent.SetDestination(_target.position);
                else if (distance < 6)
                    _agent.SetDestination(Vector2.MoveTowards(transform.position, _target.position, -1));
            } else {
                _agent.SetDestination(_target.position);
            }
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
