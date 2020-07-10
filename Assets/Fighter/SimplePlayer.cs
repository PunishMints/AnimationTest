using UnityEngine;
using System.Collections;

public class SimplePlayer : MonoBehaviour
{

    Animator animator;
    Camera playerCamera;
    CharacterController controller;

    float gravity = -9.81f;
    private float verticalSpeed;

    public Transform punchTarget;

    float moveSpeed = 10f;
    float rotateSpeed = 100f;
    float cameraSpeed = 40f;


    bool left;
    bool right;
    bool strong;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        bool left = Input.GetButtonDown("Left");
        bool right = Input.GetButtonDown("Right");
        bool strong = Input.GetButtonDown("Strong");

        UpdateMovement();
        UpdateRotation();
        //UpdateCamera();

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
        animator.MatchTarget(punchTarget.position, punchTarget.rotation, AvatarTarget.LeftHand,
                                                       new MatchTargetWeightMask(Vector3.one, 1f), 0.141f, 0.78f);
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

        Vector3 vel = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;

        if (controller.isGrounded)
        {
            verticalSpeed = 0;
        }

        verticalSpeed += (gravity * Time.deltaTime);
        vel.y = verticalSpeed * Time.deltaTime;

        controller.Move(vel);
    }

    void UpdateRotation()
    {
        float horizontalInput = Input.GetAxis("Mouse Y");

        float verticalInput = Input.GetAxis("Mouse X");

        transform.Rotate(new Vector3(horizontalInput * -1, verticalInput, 0) * rotateSpeed * Time.deltaTime);
    }

    void UpdateCamera()
    {
        float horizontalInput = Input.GetAxis("Mouse Y");

        float verticalInput = Input.GetAxis("Mouse X");

        playerCamera.transform.Rotate(new Vector3(horizontalInput*-1, verticalInput, 0) * cameraSpeed * Time.deltaTime);
    }

}
