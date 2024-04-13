using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public enum EnemyState
{
    Patrolling,
    Fighting,
    Attacking
}

public class EnemyController : MonoBehaviour
{

        
    [Header("Enemy Stats")] 
    [SerializeField] private int _enemyHealth = 100;
    [SerializeField] private int _enemyMaxHealth = 100;
    [SerializeField] private bool _isEnemyDead;
    [SerializeField] private float _enemySpeed;
    
    [Header("Invincible")] 
    [SerializeField] private float _invincibleTime = 10f;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private bool _canInvincible = true;
    [SerializeField] private float _invincibleCooldownTime = 3.0f;
    
    [Header("Attack")] 
    [SerializeField] private float _attackRange = 1.3f;
    [SerializeField] private int _attackDMG = 3;
    [SerializeField] private bool _canAttack = true;
    [SerializeField] private float _attackingCooldownTime = 3.0f;
    
    [Header("NavMash_Setting")]
    private NavMeshAgent _agentAi;
    public Vector3 pointOfInterest;
    [SerializeField]
    private float wiggleRoom = 1.25f;
    private LocationManager _locationManager;

    [Header("Target")]
    private Transform _player;
    private float _enemyHitRange;


    private Animator _enemyAnim;
    public EnemyState _currentState = EnemyState.Fighting;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _player = FindObjectOfType<Player>().transform;
        _enemyAnim = GetComponentInChildren<Animator>();
        _locationManager = FindObjectOfType<LocationManager>();
        _agentAi = GetComponent<NavMeshAgent>();
        _agentAi.updateRotation = false;
        _agentAi.updateUpAxis = false;
        _agentAi.avoidancePriority = 0;
        _agentAi.speed = _enemySpeed;
        
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
                if (!IsTargetInRange())
                {
                    _agentAi.SetDestination(_player.position);
                    TravelToDestination();
                }
                else
                {
                    _currentState = EnemyState.Attacking;
                }
                break;
            case EnemyState.Attacking:
                if (IsTargetInRange())
                {
                    _agentAi.ResetPath();

                    if (_canAttack)
                    { 
                        StartCoroutine(Attack()); 
                    } 
                }
                else
                {
                    _currentState = EnemyState.Fighting;
                }
                break;
        }
    }
    
    private void SetAnimation()
    {
        
        if (_agentAi.velocity.x != 0 || _agentAi.velocity.y != 0)
        {
            float x = _agentAi.velocity.x;
            float y = _agentAi.velocity.y;
            _enemyAnim.SetFloat("moveX", x);
            _enemyAnim.SetFloat("moveY", y);
        }
    }

    public bool IsTargetInRange()
    {
        float distance = Vector3.Distance(transform.position, _player.position);
        return distance < _attackRange;
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

    public IEnumerator Attack()
    {
        _canAttack = false;
        HitPlayer();
        _enemyAnim.Play("Attack");
        Debug.Log("Attack");
        yield return new WaitForSeconds(_attackingCooldownTime);
        Debug.Log("Done Attacking");
        _canAttack = true;
    }
    

    void HitPlayer()
    {
        _player.GetComponent<Player>().TakeDMG(_attackDMG);
    }
    
    public void TakeDMG(int value)
    {
        if (!_isInvincible)
        {
            this._enemyHealth -= value;
            if (_canInvincible)
            {
                StartCoroutine(SetInvinciblility());
            }
        }
        
        if (_enemyHealth <= 0)
        {
            _isEnemyDead = true;
        }
    }
    
    private IEnumerator SetInvinciblility()
    {
        _isInvincible = true;
        _canInvincible = false;
        yield return new WaitForSeconds(_invincibleTime);
        _isInvincible = false;
        yield return new WaitForSeconds(_invincibleCooldownTime);
        _canInvincible = true;
    }
    
}
