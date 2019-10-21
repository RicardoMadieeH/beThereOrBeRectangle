using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camController : MonoBehaviour
{

    public Transform playerCam, character, centerPoint;

    CharacterController player;
    Transform playerTransform;

    private float mouseYPosition,Sensitivity,zoomSpeed,zoomMin,zoomMax,walkSpeed,rotationSpeed,verticalVelocity,jumpDist, sprintSpeed, Speed;
    private float zoom,rotX, rotY,moveFB, moveLR;
    int jumpTimes;

    void Start()
    {
        Speed = 2f;
        mouseYPosition = 1f;
        Sensitivity = 10f;
        zoomSpeed = 2;
        zoomMin = -2f;
        zoomMax = -10f;
        walkSpeed = 2f;
        rotationSpeed = 5f;
        jumpDist = 5f;
        player = GameObject.Find("Player").GetComponent<CharacterController>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        zoom = -3;
        sprintSpeed = 3f;
    }

    void Update()
    {
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (zoom > zoomMin) zoom = zoomMin;
        if (zoom < zoomMax) zoom = zoomMax;

        playerCam.transform.localPosition = new Vector3(0, 0, zoom);

        rotX += Input.GetAxis("Mouse X") * Sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * Sensitivity;

        rotY = Mathf.Clamp(rotY, -60f, 60f);
        playerCam.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(rotY, rotX, 0);

        if (Input.GetKey("left shift")) Speed = sprintSpeed;

        moveFB = Input.GetAxis("Vertical") * Speed;
        moveLR = Input.GetAxis("Horizontal") * Speed;
        Vector3 movement = new Vector3(moveLR, verticalVelocity, moveFB);

        movement = character.rotation * movement;

        player.Move(movement * Time.deltaTime);

        if (!Input.GetKey("left shift") && Speed > walkSpeed) Speed -= 0.01f;

        centerPoint.position = new Vector3(character.position.x, character.position.y + mouseYPosition, character.position.z);

        Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
        character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);

        if (player.isGrounded) jumpTimes = 0;
        if (jumpTimes < 2 && Input.GetButtonDown("Jump"))
        {
            verticalVelocity += jumpDist;
            jumpTimes++;
        }

        if (Input.GetKeyDown("left ctrl"))
        {
            playerTransform.transform.localScale = new Vector3(1, 0.5F, 1);
        }
        if (Input.GetKeyUp("left ctrl"))
        {
            playerTransform.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        verticalVelocity = player.isGrounded ? 0f : verticalVelocity += Physics.gravity.y * Time.deltaTime;
    }
}