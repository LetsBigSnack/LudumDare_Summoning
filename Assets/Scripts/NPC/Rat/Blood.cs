using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField] private float _TTD = 10.0f;
    
    
    private int _bloodValue = 1;
    
    [SerializeField]
    private Transform _player;
    
    [SerializeField]
    private float _speed = 0.25f;


    public void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        StartCoroutine(Die());
    }

    public void FixedUpdate()
    {
        if (_player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.fixedDeltaTime);
        }
    }

    public int GetBlood()
    {
        return _bloodValue;
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(_TTD);
        Destroy(gameObject);
    }
    
}
