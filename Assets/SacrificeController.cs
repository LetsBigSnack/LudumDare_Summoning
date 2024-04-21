using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeController : MonoBehaviour
{
    public GameObject prefab;

    public void InstantiateBlood()
    {
        GameObject blood = Instantiate(prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
