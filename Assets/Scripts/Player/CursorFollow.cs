using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollow : MonoBehaviour
{

    public bool isGamepadUsed = false;
    private Vector2 cursorPosition;
    private PlayerInput playerControls;
    private Camera mainCamera;

    private void Awake()
    {
        playerControls = new PlayerInput();
        mainCamera = Camera.main;
        
        playerControls.Player.MouseMovement.performed += ctx =>
        {
            isGamepadUsed = false;
            Vector2 inputPos = ctx.ReadValue<Vector2>();
            cursorPosition = inputPos;
        };
        
        playerControls.Player.GamepadCursorMove.performed += ctx =>
        {
            isGamepadUsed = true;
            Vector2 inputPos = ctx.ReadValue<Vector2>();
            cursorPosition = inputPos;
        };
        
        playerControls.Player.GamepadCursorMove.canceled += ctx =>
        {
            Debug.Log("TWITTER");
            isGamepadUsed = false;
        };
        
        

        playerControls.Enable();
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Vector3.zero;
        if (!isGamepadUsed)
        {
            Vector3 screenPosition = new Vector3(cursorPosition.x, cursorPosition.y, 0);
            float targetDepth = (transform.position - mainCamera.transform.position).z;
            screenPosition.z = targetDepth;
            targetPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        }
        else
        {
            Vector3 deltaMove = new Vector3(cursorPosition.x, cursorPosition.y, 0) * Time.deltaTime * 5;
            targetPosition = transform.position + deltaMove;
        }

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