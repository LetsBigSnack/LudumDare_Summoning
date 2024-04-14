using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float _remainingTime = 120;
    
    // Update is called once per frame
    void Update()
    {
        _remainingTime -= Time.deltaTime;
    }

    public void AddTime(float addedTime)
    {
        _remainingTime += addedTime;
    }

    public float GetTime()
    {
        return _remainingTime;
    }
}
