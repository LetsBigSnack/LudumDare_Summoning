using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SummonState
{
    Idling,
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
    [SerializeField] private int _bloodCost = 10;

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
    public AudioSource[] attackSound;

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

    [Header("DamageIndication")]
    public Material baseShader;
    public Material dmgShader;
    private SpriteRenderer _spriteRenderer;
    public bool isDamaged;

    // Start is called before the first frame update
    void Start()
    {
        
        if(_hero == null)
        {
            if (FindObjectOfType<EnemyController>() != null)
            {
                _hero = FindObjectOfType<EnemyController>().transform;
            }
            else
            {
                _hero = null;
            }
        }
        if (!_isMelee)
        {
            _spawner = GetComponentInChildren<ProjectileSpawner>();
        }
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _summonAnim = GetComponentInChildren<Animator>();
        _agentAi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _agentAi.updateRotation = false;
        _agentAi.updateUpAxis = false;
        _agentAi.avoidancePriority = 0;
        _agentAi.speed = _summonSpeed;
        transform.rotation = Quaternion.identity;

    }

    private void Update()
    {
        SetAnimation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(_hero == null)
        {
            if (FindObjectOfType<EnemyController>() != null)
            {
                _hero = FindObjectOfType<EnemyController>().transform;
            }
            else
            {
                _hero = null;
            }
        }

        if (_hero != null)
        {

            switch (_currentState)
            {
                case SummonState.Idling:

                    if (IsTargetInRange() && !_canAttack)
                    {
                        _summonAnim.SetBool("isMoving", false);
                    }
                    else if (IsTargetInRange() && _canAttack)
                    {
                        _currentState = SummonState.Attacking;
                    } 
                    else if(!IsTargetInRange())
                    {
                        _currentState = SummonState.Attacking;
                    }

                    break;

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
                                _summonAnim.SetBool("isMoving", false);
                                StartCoroutine(Attack());
                            }
                        }
                        else if(!IsTargetInRange())
                        {
                            _currentState = SummonState.Following;
                        } 
                        else if (IsTargetInRange() && !_canAttack)
                        {
                            _currentState = SummonState.Idling;
                        }
                    }

                    break;
            }
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
        attackSound[Random.Range(0, attackSound.Length - 1)].Play();
        yield return new WaitForSeconds(_attackingCooldownTime);
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
            StartCoroutine(FlashDMG());
            if (_canInvincible)
            {
                StartCoroutine(SetInvinciblility());
            }
        }

        if (_summonHealth <= 0)
        {
            _isSummonDead = true;
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashDMG()
    {
        if (!isDamaged)
        {
            isDamaged = true;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = dmgShader;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = baseShader;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = dmgShader;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.material = baseShader;
            isDamaged = false;
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

    public int GetBloodCost()
    {
        return _bloodCost;
    }

}


