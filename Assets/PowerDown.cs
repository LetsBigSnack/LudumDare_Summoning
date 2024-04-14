using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDown : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public string titel;
    [SerializeField] public string description;
    [SerializeField] public float amountToChange;
    [SerializeField] public string valueToChange;

    private PowerUpManager _pwManager;

    public void Start()
    {

        _pwManager = FindObjectOfType<PowerUpManager>(true);

        if (_pwManager != null)
        {
            switch (valueToChange)
            {
                case "health":
                    _pwManager.heroHealth += (int)amountToChange;
                    break;
                case "attack":
                    _pwManager.heroDamage += (int)amountToChange;
                    break;
                case "speed":
                    _pwManager.heroSpeed += amountToChange;
                    break;
                case "dps":
                    _pwManager.heroAttackSpeed += amountToChange;
                    break;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("I add " + valueToChange + amountToChange);
        Destroy(gameObject, 1);
    }

}
