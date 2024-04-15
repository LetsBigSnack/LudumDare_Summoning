using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{

    private bool levelManagerDisplayed;
    public GameObject _levelUpManager;

    private PauseManager _pauseManager;

    public Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _pauseManager = FindObjectOfType<PauseManager>();
        _levelUpManager = FindObjectOfType<LeveLUpLookUp>(true).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.hasLeveledUp)
        {
            _levelUpManager.SetActive(true);
            _pauseManager.HardPauseGame();
            levelManagerDisplayed = true;
        }
        else
        {
            if (levelManagerDisplayed)
            {
                _levelUpManager.SetActive(false);
                _pauseManager.ResumeGame();
                levelManagerDisplayed = false;
            }
        }
    }
}
