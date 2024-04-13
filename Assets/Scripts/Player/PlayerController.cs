using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 10f;
    
    private PlayerInput _input;
    private Player _player;
    private Vector2 _playerMoveVector = Vector2.zero;
    private Rigidbody2D _playerRB;
    private Animator _playerAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GetComponentInChildren<Animator>();
        _input = new PlayerInput();
    }

    private void Awake()
    {
        _input = new PlayerInput();
        _player = FindObjectOfType<Player>();
        _playerRB = _player.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _input.Enable();
        // Movement
        _input.Player.Movement.performed += OnMovePlayerPerfomred;
        _input.Player.Movement.canceled += OnMovePlayerCancelled;
    }

    private void OnDisable()
    {
        _input.Disable();
        // Movement
        _input.Player.Movement.performed -= OnMovePlayerPerfomred;
        _input.Player.Movement.canceled -= OnMovePlayerCancelled;
        _input.Player.Movement.Disable();
        
    }

    private void OnDestroy()
    {
        _input.Disable();
        // Movement
        _input.Player.Movement.performed -= OnMovePlayerPerfomred;
        _input.Player.Movement.canceled -= OnMovePlayerCancelled;
        _input.Player.Movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.HasPlayerLost())
        {
            MovePlayer();
            float x = _playerMoveVector.x;
            float y = _playerMoveVector.y;
        }
        else
        {
            _playerMoveVector = Vector2.zero;
            _playerRB.velocity = Vector2.zero;
        }
       
    }

    // Player Movement
    void MovePlayer()
    {
        _playerRB.velocity = _playerMoveVector * playerSpeed;
    }
    
    void OnMovePlayerPerfomred(InputAction.CallbackContext value)
    {

        _playerMoveVector = value.ReadValue<Vector2>();
        _player.isMoving = true;
    }

    void OnMovePlayerCancelled(InputAction.CallbackContext value)
    {
        _playerMoveVector = Vector2.zero;
        _player.isMoving = false;
    }
    
}
