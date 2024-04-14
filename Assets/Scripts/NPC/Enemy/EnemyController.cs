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
    Walking,
    Attacking
}

public class EnemyController : MonoBehaviour
{

    private PowerUpManager _powerUpManager;
        
    [Header("Enemy Stats")] 
    [SerializeField] private int _enemyHealth = 100;
    [SerializeField] private int _enemyMaxHealth = 100;
    [SerializeField] private bool _isEnemyDead;
    [SerializeField] private float _enemySpeed;
    
    [Header("Invincible")] 
    [SerializeField] private float _invincibleTime = 0.5f;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private bool _canInvincible = true;
    [SerializeField] private float _invincibleCooldownTime = 1.0f;
    
    [Header("Attack")] 
    [SerializeField] private float _attackRange = 2.0f;
    [SerializeField] private int _attackDMG = 3;
    [SerializeField] private bool _canAttack = true;
    [SerializeField] private float _attackingCooldownTime = 3.0f;
    
    [Header("NavMash_Setting")]
    private NavMeshAgent _agentAi;
    [SerializeField]
    private float wiggleRoom = 1.25f;
    private LocationManager _locationManager;

    [Header("Target")] 
    [SerializeField] private List<GameObject> _possibleTargets;
    [SerializeField] private Transform _target;
    private float _enemyHitRange;


    private Animator _enemyAnim;
    public EnemyState _currentState = EnemyState.Walking;



    public void SetHealth(int value)
    {
        this._enemyHealth += value;
        this._enemyMaxHealth += value;
    }
    
    public void SetSpeed(float value)
    {
        this._enemySpeed += value;
    }
    
    public void SetDMG(int value)
    {
        this._attackDMG += value;
    }
    
    public void SetAttackSpeed(float value)
    {
        this._attackingCooldownTime -= value;
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {   
        _powerUpManager = FindObjectOfType<PowerUpManager>(true);
        _powerUpManager.applySet(this);
        _possibleTargets = new List<GameObject>();
        _target = FindObjectOfType<Player>().transform;
        _enemyAnim = GetComponentInChildren<Animator>();
        _locationManager = FindObjectOfType<LocationManager>();
        _agentAi = GetComponent<NavMeshAgent>();
        _agentAi.updateRotation = false;
        _agentAi.updateUpAxis = false;
        _agentAi.avoidancePriority = 0;
        _agentAi.speed = _enemySpeed;
    }

    private void Update()
    {
        UpdateTargets();
        SetAnimation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        
        if (_possibleTargets.Count > 0)
        {
            _target = FindPriorityTarget();
        }
        else
        {
            _currentState = EnemyState.Walking;
        }
        
        switch (_currentState)
        {
            case EnemyState.Walking:
                
                if (_target && !IsTargetInRange(_target))
                {
                    _agentAi.SetDestination(_target.position);
                }
                else
                {
                    _currentState = EnemyState.Attacking;
                }
                break;
            case EnemyState.Attacking:
                if (_target && IsTargetInRange(_target))
                {
                    _agentAi.ResetPath();
                    Debug.Log("ATTACK");
                    if (_canAttack)
                    { 
                        StartCoroutine(Attack()); 
                    } 
                }
                else
                {
                    _agentAi.SetDestination(_target.position);
                    _currentState = EnemyState.Walking;
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
    
    public bool IsTargetInRange(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < _attackRange;
    }
    
    
    public IEnumerator Attack()
    {
        _canAttack = false;
        _enemyAnim.Play("Attack");
        yield return new WaitForSeconds(0.2f);
        HitObject();
        yield return new WaitForSeconds(_attackingCooldownTime);
        _canAttack = true;
    }
    

    void HitObject()
    {
        Debug.Log(_target.tag);
        if (_target.CompareTag("Player"))
        { 
            _target.GetComponent<Player>().TakeDMG(_attackDMG);
        }
        else
        {
            _target.GetComponent<SummonsController>().TakeDMG(_attackDMG);
        }
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
            Destroy(gameObject);
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

    public void AddTarget(GameObject obj)
    {
        _possibleTargets.Add(obj);
    }
    
    public void RemoveTarget(GameObject obj)
    {
        _possibleTargets.Remove(obj);
    }
    
    void UpdateTargets()
    {
        _possibleTargets = _possibleTargets.Where(item => item != null).ToList();
    }
    
    private Transform FindPriorityTarget()
    {
        GameObject player = _possibleTargets.FirstOrDefault(t => t.CompareTag("Player"));
        if (player)
            return player.transform;
        
        return _possibleTargets.FirstOrDefault()?.transform;
    }
    
}
