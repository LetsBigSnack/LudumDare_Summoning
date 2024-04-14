using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SummonState
{
    Following,
    Attacking
}

public class SummonsController : MonoBehaviour
{


    [Header("Summon Stats")]
    [SerializeField] private int _summonHealth = 100;
    [SerializeField] private int _summonMaxHealth = 100;
    [SerializeField] private bool _isSummonDead;
    [SerializeField] private float _summonSpeed;

    [Header("Invincible")]
    [SerializeField] private float _invincibleTime = 10f;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private bool _canInvincible = true;
    [SerializeField] private float _invincibleCooldownTime = 3.0f;

    [Header("Attack")]
    [SerializeField] private float _attackRange = 1.3f;
    [SerializeField] private int _attackDMG = 3;
    [SerializeField] public bool _canAttack = true;
    [SerializeField] private float _attackingCooldownTime = 3.0f;

    [Header("NavMash_Setting")]
    private UnityEngine.AI.NavMeshAgent _agentAi;
    [SerializeField]
    private float wiggleRoom = 1.25f;

    [Header("Target")]
    private Transform _hero;
    private float _summonHitRange;

    public bool _isMelee;
    private ProjectileSpawner _spawner;
    private Animator _summonAnim;
    public SummonState _currentState = SummonState.Following;

    // Start is called before the first frame update
    void Start()
    {

        _hero = FindObjectOfType<EnemyController>().transform;
        if (!_isMelee)
        {
            _spawner = GetComponentInChildren<ProjectileSpawner>();
        }
        _summonAnim = GetComponentInChildren<Animator>();
        _agentAi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _agentAi.updateRotation = false;
        _agentAi.updateUpAxis = false;
        _agentAi.avoidancePriority = 0;
        _agentAi.speed = _summonSpeed;

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
            case SummonState.Following:

                if (!IsTargetInRange())
                {
                    _agentAi.SetDestination(_hero.position);
                }
                else
                {
                    _currentState = SummonState.Attacking;
                }
                
                break;
            case SummonState.Attacking:
                if (!IsTargetInRange())
                {
                    _agentAi.SetDestination(_hero.position);
                }
                else
                {
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
                        _currentState = SummonState.Following;
                    }
                    break;
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
            _summonAnim.SetFloat("moveX", x);
            _summonAnim.SetFloat("moveY", y);
        }
    }

    public bool IsTargetInRange()
    {
        float distance = Vector3.Distance(transform.position, _hero.position);
        return distance < _attackRange;
    }

    public IEnumerator Attack()
    {
        _canAttack = false;
        if (_isMelee)
        {
            HitHero();
        } else
        {
            StartCoroutine(ShootArrow());
        }
        _summonAnim.Play("Attack");
        Debug.Log("Attack");
        yield return new WaitForSeconds(_attackingCooldownTime);
        Debug.Log("Done Attacking");
        _canAttack = true;
    }


    void HitHero()
    {
        _hero.GetComponent<EnemyController>().TakeDMG(_attackDMG);
    }

    public void TakeDMG(int value)
    {
        if (!_isInvincible)
        {
            this._summonHealth -= value;
            if (_canInvincible)
            {
                StartCoroutine(SetInvinciblility());
            }
        }

        if (_summonHealth <= 0)
        {
            _isSummonDead = true;
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

    private IEnumerator ShootArrow()
    {
        yield return new WaitForSeconds(0.3f);
        _spawner.SpawnProjectile();
    }

}


