using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void applySet(EnemyController hero)
    {
        //apply the changes to the new hero
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
