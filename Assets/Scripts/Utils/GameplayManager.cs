using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    private Player _player;
    private TimeManager _timeManager;
    [SerializeField] private bool _isGameOver = false;
    private DeathUiManager _deathUiManager;
    private CursorFollow _cursorFollow;
    private SummonManager _summonManager;
    
    void Start()
    {
        _deathUiManager = FindObjectOfType<DeathUiManager>(true);
        _cursorFollow = FindObjectOfType<CursorFollow>();
        _summonManager = FindObjectOfType<SummonManager>();
        _player = FindObjectOfType<Player>();
        _timeManager =  FindObjectOfType<TimeManager>();
    }

    void Update()
    {
        if (_player.GetHealth() <= 0 || _timeManager.GetTime() <= 0)
        {
            _isGameOver = true;
            _deathUiManager.gameObject.SetActive(true);
            _cursorFollow.gameObject.SetActive(false);
            _summonManager.gameObject.SetActive(false);
            Debug.Log("GAME OVER");
            Time.timeScale = 0;
        } 
    }
}
