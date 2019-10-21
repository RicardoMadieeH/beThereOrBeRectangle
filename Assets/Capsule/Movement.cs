using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float gravity = 10;
    public float thrust = 20;
    public float jumpspeed = 200;
    public Rigidbody rb;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * Input.GetAxis("Vertical") * thrust, ForceMode.Force);
        rb.AddForce(transform.right * Input.GetAxis("Horizontal") * thrust, ForceMode.Force);

        if(controller.isGrounded)
        {
            rb.AddForce(transform.up * Input.GetAxis("Jump") * jumpspeed, ForceMode.Force);
        }


        //rb.AddForce(transform.up * Input.GetAxis("Jump") * thrust, ForceMode.Force);
        //if (controller.isGrounded)
        //{
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //  rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        //}
        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
        //rb.AddForce(transform.up * Input.GetAxis("Jump") * thrust, ForceMode.Impulse);
        //}
        //else
        //{
        //    rb.AddForce(transform.up * gravity * -1, ForceMode.Acceleration);
        //}

    }
}
