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
        mainCamera = Camera.main; // Cache the main camera for better performance

        // Attach input action for cursor movement
        playerControls.Player.CursorMovement.performed += ctx => 
        {
            cursorPosition = ctx.ReadValue<Vector2>(); // Directly reading the value for both mouse and gamepad
        };
        playerControls.Player.CursorMovement.canceled += ctx => cursorPosition = Vector2.zero;

        playerControls.Enable();
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Vector3.zero;

        if (Gamepad.current == null)
        {
            // Convert screen coordinates to world position
            Vector3 screenPosition = new Vector3(cursorPosition.x, cursorPosition.y, 0);
            float targetDepth = (transform.position - mainCamera.transform.position).z;
            screenPosition.z = targetDepth;
            targetPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        }
        else
        {
            // Use gamepad input to adjust position (considered delta movements)
            Vector3 deltaMove = new Vector3(cursorPosition.x, cursorPosition.y, 0) * Time.deltaTime * 5; // Scale movement
            targetPosition = transform.position + deltaMove;
        }

        // Clamp the position to ensure it stays within the visible screen
        transform.position = ClampToWorldSpace(targetPosition);
    }

    private Vector3 ClampToWorldSpace(Vector3 targetPosition)
    {
        Vector3 minScreenBounds = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, targetPosition.z));
        Vector3 maxScreenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, targetPosition.z));

        float clampedX = Mathf.Clamp(targetPosition.x, minScreenBounds.x, maxScreenBounds.x);
        float clampedY = Mathf.Clamp(targetPosition.y, minScreenBounds.y, maxScreenBounds.y);

        return new Vector3(clampedX, clampedY, targetPosition.z);
    }

    private void OnDestroy()
    {
        playerControls.Disable();
    }
}
