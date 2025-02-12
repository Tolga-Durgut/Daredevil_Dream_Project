using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimationManager animationManager;
    public Vector2 movementInput;
    public Vector2 cameraInput;
    
    public float cameraInputX;
    public float cameraInputY;

    private float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    private void Awake() 
    {
        animationManager = GetComponent<AnimationManager>();
    }
    private void OnEnable() {
        if (playerControls == null) 
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable() {

        playerControls.Disable();
    }

    public void HandleAllInputs() 
    {
        HandleMovementInput();
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animationManager.UpdateAnimatorValues(0,moveAmount);
    }

}
