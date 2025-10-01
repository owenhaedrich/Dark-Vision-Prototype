using UnityEngine;

public class Movement : MonoBehaviour
{
    UnityEngine.Vector2 hInput;
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float speed = 11f;
    [SerializeField] float gravity = -9.81f;
    
    UnityEngine.Vector3 verticalVelocity = UnityEngine.Vector3.zero;
    

    public void Start()
    {
        
    }


    // Update the camera and inputs every frame
    public void Update()
    {
        //Horizontal Movement
        UnityEngine.Vector3 camForward = cameraTransform.forward;
        UnityEngine.Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();
        UnityEngine.Vector3 moveDir = (camRight * hInput.x + camForward * hInput.y).normalized;


        UnityEngine.Vector3 horizontalVelocity = moveDir * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);




        //Vertical Movement
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        //ground check
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1.2f, Color.purple);
            verticalVelocity.y = 0;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1.2f, Color.red);
        }
    }


    public void ReceiveInput(UnityEngine.Vector2 _hInput)
    {
        hInput = _hInput;
        //print(hInput);
    }
}