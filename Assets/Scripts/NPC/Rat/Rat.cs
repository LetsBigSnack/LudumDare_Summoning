using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{


    public GameObject prefabBlood;
    
    
    [SerializeField] private bool _isBeingSacrificed = false;
    [SerializeField] private float sacrificeTime = 2.0f;
    


    public void StartSacrifice()
    {
        if (!_isBeingSacrificed)
        {
            _isBeingSacrificed = true;
            StartCoroutine(GetSacrificed()); 
        }
    }

    public bool GetBeingSacrificed()
    {
        return _isBeingSacrificed;
    }
    
    public IEnumerator GetSacrificed()
    {
        yield return new WaitForSeconds(sacrificeTime);
        //Spawn Blood
        
        Vector3 pos = transform.position;
        Quaternion rotation = transform.rotation;

        Instantiate(prefabBlood, pos, rotation);
        
        Debug.Log("Spawning Blood");
        Destroy(gameObject);
    }
    
}
