using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [Header("Attributes")]
    public GameObject projectile;
    // Start is called before the first frame update
    
    public void SpawnProjectile()
    {
        Debug.Log("I was called");
        GameObject Projectile = Instantiate(projectile);
        Projectile.transform.position = this.transform.position;
    }
}
