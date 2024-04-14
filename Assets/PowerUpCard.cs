using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpCard : MonoBehaviour
{
    [Header("PowerUpMenu")]
    public GameObject powerUpMenu;

    [Header("PowerUp")]
    public PowerUp powerUp;

    [Header("PowerDown")]
    public PowerDown powerDown;

    [Header("CardAttributesPos")]
    public TextMeshProUGUI posTitel;
    public TextMeshProUGUI posDescription;

    [Header("CardAttributesNeg")]
    public TextMeshProUGUI negTitel;
    public TextMeshProUGUI negDescription;

    // Start is called before the first frame update

    private void OnDisable()
    {
        powerUp = null;
        powerDown = null;
    }
    void Start()
    {
        posTitel.text = powerUp.titel;
        posDescription.text = powerUp.description;

        negTitel.text = powerDown.titel;
        negDescription.text = powerDown.description;
    }

    // Update is called once per frame
   
    public void PickPower()
    {
        GameObject powerUpPrefab = Instantiate(powerUp.gameObject);
        GameObject powerDownPrefab = Instantiate(powerDown.gameObject);
        closeMenu();
    }

    public void closeMenu()
    {
        powerUpMenu.SetActive(false);
    }
}
