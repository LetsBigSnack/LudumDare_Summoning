using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{

    public bool isPaused = false;
    public bool canResume = true;
    private PlayerInput _input;
    private CursorFollow _cursorFollow;
    private SummonManager _summonManager;
    
    public void Awake()
    { 
        _input = new PlayerInput();
        _cursorFollow = FindObjectOfType<CursorFollow>();
        _summonManager = FindObjectOfType<SummonManager>();
    }

    private void OnEnable()
    {
        _input.Enable();
        
        // ToggleSummon
        _input.Player.TogglePause.performed += TogglePause;

    }

    private void OnDisable()
    {
        _input.Disable();
        // ToggleSummon
        _input.Player.TogglePause.performed -= TogglePause;
        _input.Player.TogglePause.Disable();
    }

    private void OnDestroy()
    {
        _input.Disable();
        // ToggleSummon
        _input.Player.TogglePause.performed -= TogglePause;
        _input.Player.TogglePause.Disable();
    }
    

    public void TogglePause(InputAction.CallbackContext value)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAA");
        if (isPaused)
        {
            Debug.Log("CCCCCCCCCCC");
            if (canResume)
            {
                Debug.Log("DDDDDDDDDDDDDDDDD");
                ResumeGame();
            }
        }
        else
        {
            Debug.Log("BBBBBBBBBBBBBBB");
            PauseGame();
        }
        
    }
    
    public void PauseGame()
    {
        _cursorFollow.gameObject.SetActive(false);
        _summonManager.gameObject.SetActive(false);
        Time.timeScale = 0;
        isPaused = true;
        canResume = true;
        Debug.Log("i am paused");
    }

    public void ResumeGame()
    {
        _cursorFollow.gameObject.SetActive(true);
        _summonManager.gameObject.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;
        canResume = true;
    }

    public void HardPauseGame()
    {
        _cursorFollow.gameObject.SetActive(false);
        _summonManager.gameObject.SetActive(false);
        Time.timeScale = 0;
        isPaused = true;
        canResume = false;
    }
}
