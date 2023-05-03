using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{
    Input_Manager input_Manager;
    Player_Manager player_Manager;
    Animator_Manager animator_Manager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    public float movementSpeed = 10;
    public float rotationSpeed = 12;

    public bool isJumping;


    public void Awake()
    {
        player_Manager = GetComponent<Player_Manager>();
        animator_Manager = GetComponent<Animator_Manager>();
        input_Manager = GetComponent<Input_Manager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement() 
    {
        HandleMovement();
        HandleRotation();
    }


    private void HandleMovement() 
    {
        moveDirection = cameraObject.forward * input_Manager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * input_Manager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        transform.Translate(movementVelocity * Time.deltaTime, Space.World);
        //playerRigidbody.velocity = movementVelocity;

        //Debug.Log(movementVelocity.y);
    }

    private void HandleRotation() 
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * input_Manager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * input_Manager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero) 
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void HandleJumping() 
    {
        
    }
}
