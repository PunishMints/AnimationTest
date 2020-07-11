using UnityEngine;
using System.Collections;

public class SimplePlayer : MonoBehaviour
{

    private Animator animator;
    public Camera playerCamera;
    private CharacterController controller;

    private float gravity = -9.81f;
    private float verticalSpeed;

    public Transform punchTarget;

    public float moveSpeed = 5f;
    public float cameraSpeed = 90f;
    public float damping = 10f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool left = Input.GetButtonDown("Left");
        bool right = Input.GetButtonDown("Right");
        bool strong = Input.GetButtonDown("Strong");

        UpdateMovement();
        UpdateRotation();
        UpdateCamera();

        if (left == true)
        {
            JabLeft();
        }

        if (right == true)
        {
            JabRight();
        }

    }

    void JabLeft()
    {
        animator.SetBool("Jab Left", true);
        Debug.Log("jab left");
    }

    void JabRight()
    {
        animator.SetBool("Jab Right", true);
        Debug.Log("jab right");
    }

    void UpdateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        var desiredMoveDirection = (forward * verticalInput + right * horizontalInput) * moveSpeed * Time.deltaTime;

        if (controller.isGrounded)
        {
            verticalSpeed = 0;
        }

        verticalSpeed += (gravity * Time.deltaTime);
        desiredMoveDirection.y = verticalSpeed * Time.deltaTime;

        controller.Move(desiredMoveDirection);
    }

    void UpdateRotation()
    {
        var lookPos = punchTarget.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    void UpdateCamera()
    {
        float rotateSpeed = Input.GetAxis("Horizontal") * cameraSpeed;
        Debug.Log(rotateSpeed);
        playerCamera.transform.LookAt(punchTarget);
        playerCamera.transform.RotateAround(punchTarget.position, new Vector3(0, 1, 0), rotateSpeed * Time.deltaTime);
    }

}
