using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Attributes")]
    public string titel;
    public string description;
    public float amountToChange;
    public string valueToChange;

    private Player _player;
    private PlayerController _playerCon;
    private SummonManager _sumMan;

    public void Start()
    {

        _player = FindObjectOfType<Player>();
        _playerCon = FindObjectOfType<PlayerController>();
        _sumMan = FindObjectOfType<SummonManager>();

        if (_player != null && _playerCon != null) { 
            switch (valueToChange)
            {
                case "health":
                    _player.SetHealth((int)amountToChange);
                    break;
                case "blood":
                    _player.SetBlood((int)amountToChange);
                    break;
                case "speed":
                    _playerCon.SetSpeed((int)amountToChange);
                    break;
                case "summon":
                    _sumMan.SetMaxSummons((int)amountToChange);
                    break;
                case "time":
                    //timemanager call
                    break;
                case "points":
                    //pointmanager call
                    break;
            }
        } else
        {
            Destroy(gameObject);
        }

        Debug.Log("I add " + valueToChange + amountToChange);
        Destroy(gameObject, 1);
    }
}
