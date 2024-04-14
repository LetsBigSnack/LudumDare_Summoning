using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SummonManager : MonoBehaviour
{
    
    
    private PlayerInput _input;
    private CursorFollow _cursorFollow;
    
    public int _summonIndex = 0;
    public List<GameObject> Summons;
    public GameObject selectedSummon;
    
    [SerializeField]
    private List<SummonsController> _spawnedSummons;

    [SerializeField]
    private int maxSummons = 30;
    
    
    public void Awake()
    { 
        _input = new PlayerInput();
        _spawnedSummons = new List<SummonsController>();
        _cursorFollow = FindObjectOfType<CursorFollow>();
        selectedSummon = Summons[0];
    }

    public void LateUpdate()
    {
        _spawnedSummons = FindObjectsByType<SummonsController>(FindObjectsSortMode.None).ToList();
    }

    private void OnEnable()
    {
        _input.Enable();
        
        // ToggleSummon
        _input.Player.ToggleSummon.performed += SwitchSummon;
        
        _input.Player.Summon.performed += SpawnSummon;

        _input.Player.ToggleUp.performed += ToggleSummonUp;
        _input.Player.ToggleDown.performed += ToggleSummonDown;

    }

    private void OnDisable()
    {
        _input.Disable();
        // ToggleSummon
        _input.Player.ToggleSummon.performed -= SwitchSummon;
        _input.Player.Summon.performed -= SpawnSummon;
        _input.Player.ToggleUp.performed -= ToggleSummonUp;
        _input.Player.ToggleDown.performed -= ToggleSummonDown;
        _input.Player.ToggleSummon.Disable();
    }

    private void OnDestroy()
    {
        _input.Disable();
        // ToggleSummon
        _input.Player.ToggleSummon.performed -= SwitchSummon;
        _input.Player.Summon.performed -= SpawnSummon;
        _input.Player.ToggleUp.performed -= ToggleSummonUp;
        _input.Player.ToggleDown.performed -= ToggleSummonDown;
        _input.Player.ToggleSummon.Disable();
    }
    
    
    void SwitchSummon(InputAction.CallbackContext value)
    {
        _summonIndex++;

        if (_summonIndex >= Summons.Count)
        {
            _summonIndex = 0;
        }

        selectedSummon = Summons[_summonIndex];
    }

    void SpawnSummon(InputAction.CallbackContext value)
    {
        if (_spawnedSummons.Count < maxSummons)
        { 
            _cursorFollow.SpawnObject(selectedSummon);
        }
    }
    
    
    void ToggleSummonUp(InputAction.CallbackContext value)
    {
        _summonIndex++;

        if (_summonIndex >= Summons.Count)
        {
            _summonIndex = 0;
        }

        selectedSummon = Summons[_summonIndex];
    }
    
    void ToggleSummonDown(InputAction.CallbackContext value)
    {
        _summonIndex--;

        if (_summonIndex < 0)
        {
            _summonIndex = Summons.Count()-1;
        }

        selectedSummon = Summons[_summonIndex];
    }
    
    public void SetMaxSummons(int value)
    {
        maxSummons += value;
    }
    
    
}
