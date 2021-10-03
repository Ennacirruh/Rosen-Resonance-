using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    public float crouchSpeed = 6.0f;
    public float walkSpeed = 6.0f;
    public float sprintSpeed = 10.0f;
    public float jumpHeight = 10.0f;
    public float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] [Range(0.0f, 0.5f)] float speedSmoothTime = 0.03f;
    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    float moveSpeed = 6f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;


    float speedVelocity = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }
    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

    }
    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
            if (Input.GetKey(KeyCode.Space))
            {
                velocityY += jumpHeight;
            }
        }

        velocityY += gravity * Time.deltaTime;

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = Mathf.SmoothDamp(moveSpeed, crouchSpeed, ref speedVelocity, speedSmoothTime);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = Mathf.SmoothDamp(moveSpeed, sprintSpeed, ref speedVelocity, speedSmoothTime);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKeyUp(KeyCode.LeftControl))
        {
            moveSpeed = Mathf.SmoothDamp(moveSpeed, walkSpeed, ref speedVelocity, speedSmoothTime);
        }


        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * moveSpeed + Vector3.up * velocityY ;
        controller.Move(velocity * Time.deltaTime);
    }
}
