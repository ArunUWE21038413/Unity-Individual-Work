using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Manager : MonoBehaviour
{
    Animator_Manager animator_Manager;
    Input_Manager input_Manager;
    Camera_Manager camera_Manager;
    PlayerLocomotion playerLocomotion;
    Rigidbody playerRigidbody;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator_Manager = GetComponent<Animator_Manager>();
        input_Manager = GetComponent<Input_Manager>();
        camera_Manager = FindObjectOfType<Camera_Manager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        //input_Manager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        //playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 5, 0);
        playerLocomotion.HandleAllMovement();
        input_Manager.HandleAllInputs();
    }

    private void LateUpdate()
    {
        camera_Manager.HandleAllCameraMovement();
    }
}
