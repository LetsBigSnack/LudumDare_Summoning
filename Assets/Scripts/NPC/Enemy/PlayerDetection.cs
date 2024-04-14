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
        Debug.Log(other.tag);
        if (other.CompareTag("Player") || other.CompareTag("Summon"))
        {
            Debug.Log(other);
            _enemyController.AddTarget(other.transform.parent.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Summon"))
        {
            _enemyController.RemoveTarget(other.transform.parent.gameObject);
        }
    }
}
