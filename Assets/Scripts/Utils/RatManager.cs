using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RatManager : MonoBehaviour
{

    private int _numberRats;
    [SerializeField] private int _ratThreshold = 100;
    [SerializeField] private float _spawnCooldown = 2.0f;
    private bool _canSpawn = true;
    
    public GameObject ratPrefab;
    
    [SerializeField]
    private List<GameObject> _spawnedRats;
    
    
    private LocationManager _locationManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _locationManager = FindObjectOfType<LocationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _spawnedRats = _spawnedRats.Where(item => item != null).ToList();
        
        if (_spawnedRats.Count < _ratThreshold && _canSpawn)
        {
            StartCoroutine(SpawnRat());
        }

    }
    
    private IEnumerator SpawnRat()
    {
        _canSpawn = false;
        Vector3 pos = _locationManager.GetRandomPointOnSpawnArea();
        Quaternion rotation = transform.rotation;
        GameObject tempRat = Instantiate(ratPrefab, pos, rotation);
        _spawnedRats.Add(tempRat);
        yield return new WaitForSeconds(_spawnCooldown);
        _canSpawn = true;
    }
}
