using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{   
    public float playerSpeed = 5;
    public float jumpHeight = 2;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayerMask;

    CharacterController player;
    Vector2 forceVector;
    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if(velocity.y < -15f)
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

        forceVector.x = Input.GetAxisRaw("Horizontal");
        forceVector.y = Input.GetAxisRaw("Vertical");
        Vector3 force = transform.right * forceVector.x + transform.forward * forceVector.y;
        if(force.magnitude > 1)
        {
            force.Normalize();
        }
        player.Move(force * playerSpeed * Time.deltaTime);
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        

        player.Move(velocity * Time.deltaTime);

        
    }
}
