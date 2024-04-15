using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private DeathUiManager _deathUiManager;
    public float rotationSpeed = 10;
    
    [Header("Sacrifice")] 
    [SerializeField] private bool _isSacrificing = false;
    [SerializeField] private float _maxSacrificeRadius = 3.0f;
    [SerializeField] public float _currentSacrificeRadius = 0.0f;
    [SerializeField] private float _sacrificeSpread = 0.5f;
    [SerializeField] public GameObject sacrificeCircle;
    public GameObject sacrificCircle;
    
    [Header("Player Stats")] 
    [SerializeField] private int _playerLevel = 1;

    [SerializeField] private int _currentXP = 0;
    [SerializeField] private int _xpToNextLevel = 100;
    public bool hasLeveledUp = false;
    [SerializeField] private int _bloodMeter;
    [SerializeField] private int _bloodMeterMax = 100;
    [SerializeField] private int _playerHealth = 100;
    [SerializeField] private int _playerMaxHealth = 100;
    [SerializeField] private bool _isPlayerDead;
    
    [Header("Invincible")] 
    [SerializeField] private float _invincibleTime = 10f;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private bool _canInvincible = true;
    [SerializeField] private float _invincibleCooldownTime = 3.0f;

    [Header("DamageIndication")]
    public Material baseShader;
    public Material dmgShader;
    private SpriteRenderer _spriteRenderer;
    public bool isDamaged;
    
    public bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _deathUiManager = FindObjectOfType<DeathUiManager>(true);
        _deathUiManager.level = _playerLevel;
        GetNewLevelThreshold();
        isMoving = false;
    }

    void GetNewLevelThreshold()
    {
        _deathUiManager.level = _playerLevel;
        _xpToNextLevel = _playerLevel * _playerLevel * 50;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isSacrificing)
        {
            GrowSacrificeCricle();
            Sacrifice();
        }
        else
        {
            ResetSacrificeCircle();
        }
    }

    public void SetSacrifice(bool value)
    {
        _isSacrificing = value;
    }
    
    public void ResetSacrificeCircle()
    {
        sacrificeCircle.SetActive(false);
        _currentSacrificeRadius = 0;
        sacrificeCircle.transform.localScale = new Vector3(_currentSacrificeRadius, _currentSacrificeRadius, 0);
        sacrificCircle.SetActive(true);
    }



    void GrowSacrificeCricle()
    {
        sacrificeCircle.SetActive(true);
        sacrificCircle.SetActive(false);
        _currentSacrificeRadius += _sacrificeSpread * Time.fixedDeltaTime;
        Transform parent = sacrificeCircle.transform.parent;
        sacrificeCircle.transform.SetParent(null);
        sacrificeCircle.transform.localScale = new Vector3(_currentSacrificeRadius, _currentSacrificeRadius, 0);
        sacrificeCircle.transform.Rotate(0,0,rotationSpeed*_currentSacrificeRadius*Time.deltaTime);
        sacrificeCircle.transform.SetParent(parent);
        if (_currentSacrificeRadius > _maxSacrificeRadius)
        {
            
            _currentSacrificeRadius = _maxSacrificeRadius;
        }
    }
    
    
    public bool HasPlayerLost()
    {
        return false;
    }

    public void AddBlood(int bloodValue)
    {
        _bloodMeter += bloodValue;
        _currentXP += bloodValue * 10;
        _deathUiManager.ratsSacrificed++;
        if (_currentXP >= _xpToNextLevel)
        {
            _currentXP = 0;
            _playerLevel++;
            hasLeveledUp = true;
            GetNewLevelThreshold();
        }
    }

    public bool SpendBlood(int value)
    {
        if (_bloodMeter - value < 0)
        {
            return false;
        }

        _bloodMeter -= value;
        return true;
    }
    
    void Sacrifice()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _currentSacrificeRadius);
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Rat"))
            {
                Rat tempRat = hitCollider.GetComponentInParent<Rat>();
                tempRat.StartSacrifice();
            }
        }
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _currentSacrificeRadius);
    }


    public void TakeDMG(int value)
    {
        if (!_isInvincible)
        {
            this._playerHealth -= value;
            StartCoroutine(FlashDMG());
            if (_canInvincible)
            {
                StartCoroutine(SetInvinciblility());
            }
        }
        
        if (_playerHealth <= 0)
        {
            _isPlayerDead = true;
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

    public int GetHealth()
    {
        return _playerHealth;
    }

    public int GetMaxHealth()
    {
        return _playerMaxHealth;
    }

    public int GetBlood()
    {
        return _bloodMeter;
    }

    public int GetMaxBlood()
    {
        return _bloodMeterMax;
    }

    public void SetHealth(int value)
    {
        _playerMaxHealth += value;
        _playerHealth = _playerMaxHealth;
    }

    public void SetBlood(int value)
    {
        _bloodMeterMax += value;
    }

    public void SetMaxSacrificeRadius(float value)
    {
        _maxSacrificeRadius += value;
    }

    public void SetSpreadSpeed(float value)
    {
        _sacrificeSpread += value;
    }

    public void LeveledUP()
    {
        hasLeveledUp = false;
    }
}
