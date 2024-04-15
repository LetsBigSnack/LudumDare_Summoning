using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{


    public GameObject prefabBlood;
    
    
    [SerializeField] private bool _isBeingSacrificed = false;
    [SerializeField] private float sacrificeTime = 2.0f;
    
    public float speed = 30f; // Speed of rotation
    public float moveSpeed = 2f; // Speed of movement
    public float levitationHeight = 1f; // Height of levitation

    public void StartSacrifice()
    {
        if (!_isBeingSacrificed)
        {
            _isBeingSacrificed = true;
            StartCoroutine(MoveAndRotateCoroutine());
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
        Quaternion rotation = Quaternion.identity;

        Instantiate(prefabBlood, pos, rotation);
        
        Debug.Log("Spawning Blood");
        Destroy(gameObject);
    }
    
    public IEnumerator MoveAndRotateCoroutine()
    {
        Vector3 startHeight = transform.position;
        Vector3 endHeight = new Vector3(transform.position.x, transform.position.y + levitationHeight, transform.position.z);
        float journey = 0f;

        // Move the rat vertically upwards to the maximum height
        while (journey <= 1f)
        {
            journey += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startHeight, endHeight, journey);
            yield return null;
        }

        // Begin spinning once the maximum height is reached
        while (_isBeingSacrificed)
        {
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
            yield return null;
        }
    }
}
