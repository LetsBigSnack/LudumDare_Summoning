using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Sacrifice")] 
    [SerializeField] private bool _isSacrificing = false;
    [SerializeField] private float _maxSacrificeRadius = 3.0f;
    [SerializeField] public float _currentSacrificeRadius = 0.0f;
    [SerializeField] private float _sacrificeSpread = 0.5f;
    [SerializeField] public GameObject sacrificeCircle;

    [Header("Player Stats")] 
    [SerializeField] private int _playerLevel = 1;
    [SerializeField] private int _bloodMeter;
    [SerializeField] private int _playerHealth = 100;
    [SerializeField] private int _playerMaxHealth = 100;
    [SerializeField] private bool _isPlayerDead;
    
    [Header("Invincible")] 
    [SerializeField] private float _invincibleTime = 10f;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private bool _canInvincible = true;
    [SerializeField] private float _invincibleCooldownTime = 3.0f;
    
    public bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
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
    }



    void GrowSacrificeCricle()
    {
        sacrificeCircle.SetActive(true);
        _currentSacrificeRadius += _sacrificeSpread * Time.fixedDeltaTime;
        Transform parent = sacrificeCircle.transform.parent;
        sacrificeCircle.transform.SetParent(null);
        sacrificeCircle.transform.localScale = new Vector3(_currentSacrificeRadius, _currentSacrificeRadius, 0);
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
    }
    
    void Sacrifice()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _currentSacrificeRadius);
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Rat"))
            {
                Debug.Log("RAT");
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
}
