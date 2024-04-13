using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [Header("Camera Settings")]
    [SerializeField] 
    private int cameraZLayer = -10;
    
    
    //Private Variables
    private Transform _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        FollowPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 playerPosition = _player.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, cameraZLayer);
    }
}