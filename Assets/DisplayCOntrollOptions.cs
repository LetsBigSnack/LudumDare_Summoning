using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DisplayCOntrollOptions : MonoBehaviour
{

    public GameObject resumeCont;
    public GameObject mainCont;
    
    private CursorFollow _cursorFollow;

    // Start is called before the first frame update
    private void Awake()
    {
        _cursorFollow = FindObjectOfType<CursorFollow>(true);
    }

    // Update is called once per frame
    void Update()
    {
        resumeCont.SetActive(_cursorFollow.isGamepadUsed);
        mainCont.SetActive(_cursorFollow.isGamepadUsed);
    }
    
}