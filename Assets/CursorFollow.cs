using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollow : MonoBehaviour
{
    private Vector2 cursorPosition;
    private PlayerInput playerControls;
    private Camera mainCamera;

    // Track the last active input device
    private bool lastInputWasGamepad = false;

    private void Awake()
    {
        playerControls = new PlayerInput();
        mainCamera = Camera.main; // Cache the main camera for performance

        // Attach input action for cursor movement
        playerControls.Player.MouseMovement.performed += ctx =>
        {
            if (!lastInputWasGamepad)
            {
                cursorPosition = ctx.ReadValue<Vector2>(); // Update cursor position from mouse
            }
        };
        playerControls.Player.MouseMovement.canceled += ctx => cursorPosition = Vector2.zero;

        playerControls.Player.GamepadCursorMove.performed += ctx =>
        {
            lastInputWasGamepad = true; // Update flag on gamepad use
            Vector2 gamepadInput = ctx.ReadValue<Vector2>();
            cursorPosition += gamepadInput * Time.deltaTime * 5; // Adjust speed as necessary
        };
        playerControls.Player.GamepadCursorMove.canceled += ctx =>
        {
            lastInputWasGamepad = false;
            cursorPosition = Vector2.zero;
        };

        playerControls.Enable();
    }

    private void LateUpdate()
    {
        // Handle different inputs
        if (!lastInputWasGamepad)
        {
            UpdatePositionFromMouse();
        }
        else
        {
            UpdatePositionFromGamepad();
        }
    }

    private void UpdatePositionFromMouse()
    {
        Vector3 screenPosition = new Vector3(cursorPosition.x, cursorPosition.y, 0);
        float targetDepth = (transform.position - mainCamera.transform.position).z;
        screenPosition.z = targetDepth;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        transform.position = ClampToWorldSpace(worldPosition);
    }

    private void UpdatePositionFromGamepad()
    {
        Vector3 deltaMove = new Vector3(cursorPosition.x, cursorPosition.y, 0) * Time.deltaTime * 5;
        Vector3 targetPosition = transform.position + deltaMove;
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
