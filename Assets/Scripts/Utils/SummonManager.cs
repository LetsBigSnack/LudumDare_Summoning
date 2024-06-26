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

    private PauseManager _pauseManager;
    private DisplaySummonManager _summonHolder;
    
    
    
    public void Awake()
    { 
        _input = new PlayerInput();
        _spawnedSummons = new List<SummonsController>();
        _cursorFollow = FindObjectOfType<CursorFollow>();
        selectedSummon = Summons[0];
        _pauseManager = FindObjectOfType<PauseManager>();
        _summonHolder = FindObjectOfType<DisplaySummonManager>(true);
    }

    private void Start()
    {
        DisplaySummon();
    }

    public void LateUpdate()
    {
        _spawnedSummons = FindObjectsByType<SummonsController>(FindObjectsSortMode.None).ToList();
        _summonHolder.currentSum.text = _spawnedSummons.Count.ToString();
        _summonHolder.maxSum.text = maxSummons.ToString();
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
        DisplaySummon();

    }

    void DisplaySummon()
    {
        _summonHolder.SetCurrent(selectedSummon.GetComponent<SummonsController>());

        if (_summonIndex + 1 >= Summons.Count)
        {
            _summonHolder.SetNext(Summons[0].GetComponent<SummonsController>());
        }
        else
        {
            _summonHolder.SetNext(Summons[_summonIndex+1].GetComponent<SummonsController>());
        }
        
        if (_summonIndex - 1 < 0)
        {
            _summonHolder.SetPrevious(Summons[Summons.Count-1].GetComponent<SummonsController>());
        }
        else
        {
            _summonHolder.SetPrevious(Summons[_summonIndex - 1].GetComponent<SummonsController>());
        }
    }

    void SpawnSummon(InputAction.CallbackContext value)
    {
        Debug.Log("------------------SUMMON");
        if (_spawnedSummons.Count < maxSummons && !_pauseManager.isPaused)
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
        DisplaySummon();
    }
    
    void ToggleSummonDown(InputAction.CallbackContext value)
    {
        _summonIndex--;

        if (_summonIndex < 0)
        {
            _summonIndex = Summons.Count()-1;
        }

        selectedSummon = Summons[_summonIndex];
        DisplaySummon();
    }
    
    public void SetMaxSummons(int value)
    {
        maxSummons += value;
    }
    
    
}
