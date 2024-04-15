using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControllerShow : MonoBehaviour
{

    public GameObject resumeCont;
    public GameObject mainCont;
    
    private CursorFollow _cursorFollow;
    private PauseManager _pauseManager;
    private PlayerInput _input;
    // Start is called before the first frame update
    private void Awake()
    {
        _input = new PlayerInput();
        _cursorFollow = FindObjectOfType<CursorFollow>(true);
        _pauseManager = FindObjectOfType<PauseManager>(true);
        _input.Enable();
        // Movement
        _input.Menu.Replay.performed += Replay;
        _input.Menu.MainMenu.performed += MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        resumeCont.SetActive(_cursorFollow.isGamepadUsed);
        mainCont.SetActive(_cursorFollow.isGamepadUsed);
    }
    
    
    void MainMenu(InputAction.CallbackContext value)
    {
        InputSystem.DisableAllEnabledActions();
        SceneManager.LoadScene("StartScreen");
    }
    
    void Replay(InputAction.CallbackContext value)
    {
        _pauseManager.ResumeGame();
    }
    
}
