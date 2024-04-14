using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    private bool _canSpawn = true;
    
    public GameObject heroPrefab;
    
    [SerializeField]
    private GameObject _spawnedHero;
    
    private LocationManager _locationManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _locationManager = FindObjectOfType<LocationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnedHero != null)
        {
            return;
        }
        
        Vector3 pos = _locationManager.GetRandomPointOnSpawnArea();
        Quaternion rotation = transform.rotation;
        _spawnedHero  = Instantiate(heroPrefab, pos, rotation);
        
    }

}
