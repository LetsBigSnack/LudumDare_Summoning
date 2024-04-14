using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    private Player _player;
    private TimeManager _timeManager;
    [SerializeField] private bool _isGameOver = false;
    

    void Start()
    {

        _player = FindObjectOfType<Player>();
        _timeManager =  FindObjectOfType<TimeManager>();
    }

    void Update()
    {
        if (_player.GetHealth() <= 0 || _timeManager.GetTime() <= 0)
        {
            _isGameOver = true;
            Debug.Log("GAME OVER");
        } 
    }
}
