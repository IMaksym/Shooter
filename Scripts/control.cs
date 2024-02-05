using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class control : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    public float speed = 6.0f;
    public float sprintSpeed = 11.0f;
    public float turnSmoothTime = 0.005f;
    float turnSmoothVelocity;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3.0f;
    private bool isSprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            
            if (isSprinting)
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = 6.0f;
            }

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        isGrounded = controller.isGrounded;
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2.0f;
        }

        controller.Move(playerVelocity * Time.deltaTime);

        if (isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }

        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void Jump()
    {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);

    }
}