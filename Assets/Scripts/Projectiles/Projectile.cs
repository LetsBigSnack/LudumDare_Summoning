using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Attributes")]
    public string itemName;
    public int damage;
    public int speed;
    public float decay;
    public float degrees;

    [Header("Target")]
    private Transform _hero;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _hero = FindObjectOfType<EnemyController>().transform;
        Shoot();
        Destroy(this.gameObject, decay);
    }

    private void Shoot() { 
        _rb.AddForce((_hero.position - transform.position).normalized*speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            HitHero();
        }
    }

    void HitHero()
    {
        _hero.GetComponent<EnemyController>().TakeDMG(damage);
    }
}
