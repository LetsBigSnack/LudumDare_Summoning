using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Sacrifice")] 
    [SerializeField] private bool _isSacrificing = false;
    [SerializeField] private float _maxSacrificeRadius = 3.0f;
    [SerializeField] private float _currentSacrificeRadius = 0.0f;
    [SerializeField] private float _sacrificeSpread = 0.5f;
    
    
    [SerializeField]
    private int _bloodMeter;

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
        _currentSacrificeRadius = 0;
    }



    void GrowSacrificeCricle()
    {
        _currentSacrificeRadius += _sacrificeSpread * Time.fixedDeltaTime;

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
                Rat tempRat = hitCollider.GetComponentInParent<Rat>();
                tempRat.StartSacrifice();
            }
        }
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _currentSacrificeRadius);
    }
    
}
