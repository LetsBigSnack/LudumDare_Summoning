using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PowerUpManager : MonoBehaviour
{


    private PlayerInput _input;

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
    void Awake()
    {
        //call this function when opening up the menu
        passPowerUp();
        _input = new PlayerInput();
    }
    
    private void OnEnable()
    {
        passPowerUp();
        
        _input.Enable();
        // Movement
        _input.Menu.PowerUp_1.performed += PickPowerUpA;
        _input.Menu.PowerUp_2.performed += PickPowerUpB;
        _input.Menu.PowerUp_3.performed += PickPowerUpC;
    }
    
    private void OnDisable()
    {
        _input.Disable();
        
        _input.Player.Movement.performed -= PickPowerUpA;
        _input.Menu.PowerUp_2.performed -= PickPowerUpB;
        _input.Menu.PowerUp_3.performed -= PickPowerUpC;
        _input.Menu.PowerUp_1.Disable();
        _input.Menu.PowerUp_2.Disable();
        _input.Menu.PowerUp_3.Disable();
    }

    private void OnDestroy()
    {
        _input.Disable();
        
        _input.Player.Movement.performed -= PickPowerUpA;
        _input.Menu.PowerUp_2.performed -= PickPowerUpB;
        _input.Menu.PowerUp_3.performed -= PickPowerUpC;
        _input.Menu.PowerUp_1.Disable();
        _input.Menu.PowerUp_2.Disable();
        _input.Menu.PowerUp_3.Disable();
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

    public void PickPowerUpA(InputAction.CallbackContext value)
    {
        powerCards[0].PickPower();
    }
    
    public void PickPowerUpB(InputAction.CallbackContext value)
    {
        powerCards[1].PickPower();
    }
    
    public void PickPowerUpC(InputAction.CallbackContext value)
    {
        powerCards[2].PickPower();
    }
    
}
