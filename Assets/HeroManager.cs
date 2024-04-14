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
    private DeathUiManager _deathUiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _deathUiManager = FindObjectOfType<DeathUiManager>(true);
        _locationManager = FindObjectOfType<LocationManager>();
        Vector3 pos = _locationManager.GetRandomPointOnSpawnArea();
        Quaternion rotation = transform.rotation;
        _spawnedHero  = Instantiate(heroPrefab, pos, rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnedHero != null)
        {
            return;
        }
        _deathUiManager.herosSlayed++;
        Vector3 pos = _locationManager.GetRandomPointOnSpawnArea();
        Quaternion rotation = transform.rotation;
        _spawnedHero  = Instantiate(heroPrefab, pos, rotation);
        
    }

}
