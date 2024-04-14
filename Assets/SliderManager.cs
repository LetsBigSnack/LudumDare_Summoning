using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Player _player;

    [Header("Sliders")]
    public Slider healthSlider;
    public Slider bloodSlider;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_player != null)
        {
            healthSlider.maxValue = _player.GetMaxHealth();
            healthSlider.value = _player.GetHealth();
            bloodSlider.maxValue = _player.GetMaxBlood();
            bloodSlider.value = _player.GetBlood();
        }
    }
}
