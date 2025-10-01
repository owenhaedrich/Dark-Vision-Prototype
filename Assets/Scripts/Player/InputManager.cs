using UnityEngine;


public class InputManager : MonoBehaviour
{
    BasicControls controls;
    BasicControls.DirectionalMovementActions movementInput;
    Vector2 hInput;
    Vector2 mouseInput;
    [SerializeField] Movement movement;
   




    private void Awake()
    {
        controls = new BasicControls();
        movementInput = controls.DirectionalMovement;


        // ctx is short for context. This tells Unity what to do when the action is performed.
        movementInput.HorizontalMovement.performed += ctx => hInput = ctx.ReadValue<Vector2>();


        // mouse input is handled by BasicControls
       // movementInput.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
       // movementInput.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }


    private void OnEnable()
    {
        controls.Enable();
    }


    private void OnDestroy()
    {
        controls.Disable();
    }


    private void Update()
    {
        movement.ReceiveInput(hInput);
    }


   
}
