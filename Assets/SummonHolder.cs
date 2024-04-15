using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonHolder : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI _price;
    private Player _player;
    
    // Start is called before the first frame update
    void Awake()
    {
        _price = GetComponentInChildren<TextMeshProUGUI>();
        _player = FindObjectOfType<Player>(true);
    }

    private void FixedUpdate()
    {
        Debug.Log("Blood"+_player.GetBlood());
        Debug.Log("Price"+ Int32.Parse(_price.text));
        if (_player.GetBlood() >= Int32.Parse(_price.text))
        {
            Debug.Log("YES");
            EnableImage();
        }
        else
        {
            Debug.Log("NO");
            DisableImage();
        }
    }

    private void OnEnable()
    {
        _price = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetPrice(string price)
    {
        _price.text = price;
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    private void DisableImage()
    {
        image.color = new Color32(68,68,68,255);
    }
    
    private void EnableImage()
    {
        image.color = new Color32(255,255,255,255);
    }
}
