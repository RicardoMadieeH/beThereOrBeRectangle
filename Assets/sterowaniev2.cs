using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sterowaniev2 : MonoBehaviour
{
    public int jumpCount;
    public float speed, jumpSpeed, gravity;
    private Vector3 moveDirection;
    private void Start()
    {
        //Variables
        speed = 6.0F;
        jumpSpeed = 100.0F;
        gravity = 200.0F;
        moveDirection = Vector3.zero;
        jumpCount = 0;
    }


    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
        if (true)
        {
           
            //Jumping
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpCount < 3)
                {
                    
                    jumpCount++;
                    moveDirection.y = jumpSpeed*jumpCount;
                }else if (controller.isGrounded)
                {
                    jumpCount = 0;
                }
            }
               
        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }
}
