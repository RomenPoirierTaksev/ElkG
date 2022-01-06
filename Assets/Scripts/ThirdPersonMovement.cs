using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;

    public float speed = 6f;

    public float mouseSensitivity = 3f;

    public GameObject viewPoint;

    public bool inverted = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayerMask;
    bool isGrounded;

    Vector2 forceVector;
    Vector3 velocity;

    public float jumpHeight = 2;
    public float gravity = -9.81f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if (velocity.y < -15f)
        {
            if (isGrounded)
            {
                velocity.y = -2f;
            }

        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (!inverted) mouseY = -mouseY;

        viewPoint.transform.rotation *= Quaternion.AngleAxis(mouseX, Vector3.up);

        viewPoint.transform.rotation *= Quaternion.AngleAxis(mouseY, Vector3.right);

        var angles = viewPoint.transform.localEulerAngles;
        angles.z = 0;

        var angle = viewPoint.transform.localEulerAngles.x;

        if(angle > 180 && angle < 340)
        {
            angles.x = 340;

        }
        else if(angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        viewPoint.transform.localEulerAngles = angles;
        transform.rotation = Quaternion.Euler(0, viewPoint.transform.rotation.eulerAngles.y, 0);
        viewPoint.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        Vector3 direction = Vector3.zero;
        direction += horizontal * transform.right;
        direction += vertical * transform.forward;

        direction.Normalize();
        if (direction.magnitude >= 0.1)
        {
            controller.Move(direction * speed * Time.deltaTime);

        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(velocity * Time.deltaTime);


    }
}
