using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    
    private EnemyController _enemyController;
    
    [Header("Player Detection Settings")]
    public float detectionRange = 5f;

    
    void Start()
    {
        _enemyController = GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyController._currentState = EnemyState.Fighting;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyController._currentState = EnemyState.Patrolling;
        }
    }
}
