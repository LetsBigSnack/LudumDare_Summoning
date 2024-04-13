using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    private Vector2 cursorPosition;

    private void Awake()
    {
        var playerControls = new PlayerInput();
        playerControls.Player.MouseMovement.performed += ctx => cursorPosition = ctx.ReadValue<Vector2>();
        playerControls.Player.MouseMovement.canceled += ctx => cursorPosition = Vector2.zero;

        playerControls.Enable();
    }

    private void Update()
    {
        Vector3 mouseScreenPosition = new Vector3(cursorPosition.x, cursorPosition.y, 0);
        float targetDepth = (transform.position - Camera.main.transform.position).z;
        mouseScreenPosition.z = targetDepth;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        transform.position = worldPosition;
    }

}
