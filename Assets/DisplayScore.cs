using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayScore : MonoBehaviour
{
    private TimeManager _timeManager;
    private DeathUiManager _deathUiManager;
    public TextMeshProUGUI time;
    public TextMeshProUGUI score;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _timeManager = FindObjectOfType<TimeManager>();
        _deathUiManager = FindObjectOfType<DeathUiManager>(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time.text = ((int)_timeManager.GetTime()).ToString();
        int scoreCalc = _deathUiManager.level * _deathUiManager.ratsSacrificed + 100 * _deathUiManager.herosSlayed;
        score.text = scoreCalc.ToString();
    }
}
