using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : MonoBehaviour
{

    [Header("Damage")]
    public int damage;


    private EnemyController _enemy;

    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            if(_enemy != null)
            {
                _enemy.TakeDMG(damage);
            } else
            {
                Destroy(gameObject);
            }
        }
    }

}
