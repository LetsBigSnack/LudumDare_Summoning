using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : MonoBehaviour
{
    
    private Player _player;
    
    void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            Blood blood = other.GetComponentInParent<Blood>();
            
            _player.AddBlood(blood.GetBlood());
            
            Destroy(blood.gameObject);
            
            Debug.Log("Yummy Blood");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            Debug.Log("Bye Blood");
        }
    }
}
