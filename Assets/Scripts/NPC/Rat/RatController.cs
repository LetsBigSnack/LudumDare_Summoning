using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    
    [Header("NavMash_Setting")]
    private UnityEngine.AI.NavMeshAgent _agentAi;
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
        _agentAi = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
        _agentAi.SetDestination(pointOfInterest);
        TravelToDestination();
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
