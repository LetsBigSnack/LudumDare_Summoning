using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [Header("Attributes")]
    public GameObject projectile;
    public Transform _hero;
    public int degrees;
    public float angle;
    // Start is called before the first frame update

    public void Start()
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
    }

    public void SpawnProjectile()
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
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector3 direction = (_hero.position - transform.position).normalized; // Calculate direction to hero.
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate the angle in degrees
            newProjectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - degrees)); // Set rotation of projectile.
        }
    }

    void Update()
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
            // Optionally update the angle continuously or when needed.
            Vector3 direction = (_hero.position - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
       
    }
}
