using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum EnemyState
{
    Patrolling,
    Fighting
}

public class EnemyController : MonoBehaviour
{
    
    private Rigidbody2D _enemyRb;
    
    [Header("NavMash_Setting")]
    private NavMeshAgent _agentAi;
    public Vector3 pointOfInterest;
    [SerializeField]
    private float wiggleRoom = 1.25f;
    private LocationManager _locationManager;

    [Header("Target")]
    private Transform _player;
    
    public EnemyState _currentState = EnemyState.Fighting;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        _player = FindObjectOfType<Player>().transform;
        _locationManager = FindObjectOfType<LocationManager>();
        _agentAi = GetComponent<NavMeshAgent>();
        _agentAi.updateRotation = false;
        _agentAi.updateUpAxis = false;
        _agentAi.avoidancePriority = 0;
        
        pointOfInterest = _locationManager.GetRandomPointOnNavMesh(this.transform);
    }

    private void Update()
    {
        SetAnimation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (_currentState)
        {
            case EnemyState.Patrolling:
                _agentAi.SetDestination(pointOfInterest);
                TravelToDestination();
                break;
            case EnemyState.Fighting:
                _agentAi.SetDestination(_player.position);
                TravelToDestination();
                break;
        }
    }
    
    private void SetAnimation()
    {
        float x = _agentAi.velocity.x;
        float y = _agentAi.velocity.y;
    }

    public void TravelToDestination()
    {
        if (HasReachedDestination())
        {
            SwitchTarget();
            _agentAi.SetDestination(pointOfInterest);
        }
    }
    
    bool HasReachedDestination()
    {
        float distance = Vector3.Distance(transform.position, pointOfInterest);
        return distance < wiggleRoom;
    }

    void SwitchTarget()
    {
        pointOfInterest = _locationManager.GetRandomPointOnNavMesh(this.transform);
    }
    
}
