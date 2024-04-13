using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollow : MonoBehaviour
{
    private Vector2 cursorPosition;
    private PlayerInput playerControls;
    private Camera mainCamera;

    private void Awake()
    {
        playerControls = new PlayerInput();
        mainCamera = Camera.main; // Cache the main camera for performance

        // Attach input action for cursor movement
        playerControls.Player.CursorMovement.performed += ctx => cursorPosition = ctx.ReadValue<Vector2>();
        playerControls.Player.CursorMovement.canceled += ctx => cursorPosition = Vector2.zero;

        playerControls.Enable();
    }

    private void LateUpdate()
    {
        // Convert cursor position to world position
        if (Gamepad.current == null)
        {
            // Handle as mouse input (screen coordinates)
            Vector3 screenPosition = new Vector3(cursorPosition.x, cursorPosition.y, 0);
            float targetDepth = (transform.position - mainCamera.transform.position).z;
            screenPosition.z = targetDepth;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
            transform.position = worldPosition;
        }
        else
        {
            // Handle as gamepad input (movement vector)
            transform.Translate(cursorPosition * Time.deltaTime * 5); // Adjust speed as necessary
        }
    }

    private void OnDestroy()
    {
        playerControls.Disable();
    }

}
