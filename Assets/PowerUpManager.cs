using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpManager : MonoBehaviour
{

    [Header("PowerUpCards")]
    public PowerUpCard[] powerCards;

    [Header("PowerUps")]
    public PowerUp[] powerUps;

    [Header("PowerDowns")]
    public PowerDown[] powerDowns;

    [Header("HeroUpgrades")]
    public int heroHealth;
    public float heroSpeed;
    public int heroDamage;
    public float heroAttackSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        //call this function when opening up the menu
        passPowerUp();
    }
    
    private void OnEnable()
    {
        passPowerUp();
    }

    public void applySet(EnemyController hero)
    {
        Debug.Log("APPLY SET");
        hero.SetHealth(heroHealth);
        hero.SetSpeed(heroSpeed);
        hero.SetDMG(heroDamage);
        hero.SetAttackSpeed(heroAttackSpeed);
    }

    public int randomNumber(int value)
    {
        return Random.Range(0, value);
    }

    public void passPowerUp()
    {
        for(int i = 0; i < powerCards.Length; i++)
        {
            powerCards[i].powerUp = powerUps[randomNumber(powerUps.Length-1)];
            powerCards[i].powerDown = powerDowns[randomNumber(powerDowns.Length-1)];
        }
    }

}
