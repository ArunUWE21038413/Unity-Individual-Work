using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    ThirdPersonControls thirdpersonControls;
    Animator_Manager animator_Manager;
    PlayerLocomotion player_locomotion;
    Rigidbody playerRigidbody;

    Animator animator;

    public Vector2 movementInput;
    
    private float moveAmount;
    private float LeftStickGap;
    private float LeftStickGap2;

    public float verticalInput;
    public float horizontalInput;

    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;
    public float jumptimer = 0;

    public bool button_south_input;
    public bool jump_input;
    public bool isjumpingOnce = false;
    public bool isJumpPressed = false;

    public bool attackInput;
    

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        //thirdpersonControls = new ThirdPersonControls();
        LeftStickGap = 0.2f;
        LeftStickGap2 = 0.8f;
        animator = GetComponent<Animator>();
        animator_Manager = GetComponent<Animator_Manager>();
        player_locomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (thirdpersonControls == null)
        {
            thirdpersonControls = new ThirdPersonControls();

            thirdpersonControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            thirdpersonControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            thirdpersonControls.PlayerMovement.Jump.started += i => jump_input = true;
            thirdpersonControls.PlayerMovement.Jump.canceled += i => jump_input = false;
            thirdpersonControls.PlayerMovement.Jump.performed += i => jump_input = true;

            thirdpersonControls.PlayerMovement.Attack.started += i => attackInput = true;
            thirdpersonControls.PlayerMovement.Attack.canceled += i => attackInput = false;
            thirdpersonControls.PlayerMovement.Attack.performed += i => attackInput = true;

        }

        thirdpersonControls.PlayerMovement.Enable();
    }

    //void onJump(InputAction.CallbackContext context) 
    //{
        //isJumpPressed = context.ReadValueAsButton();
        //Debug.Log(isJumpPressed);


    //}

    private void Attack() 
    {
        if (attackInput)
        {
            Debug.Log("Attack works");
            animator.SetInteger("Whatever", 3);
        }
    }

    private void Jumping() 
    {
            isjumpingOnce = true;
            jump_input = false;
            Vector3 jumpingVelocity = new Vector3(0, 1, 0);
            jumpingVelocity.y = jumpingVelocity.y * 5;
            playerRigidbody.velocity += jumpingVelocity;
            animator.SetInteger("Whatever", 7);
     
    }

    private void OnDisable()
    {
        thirdpersonControls.Disable();

    }

    public void HandleAllInputs() 
    {
        Attack();
        HandleMovementInput();
        HandleJumpingInput();
        //HandleActionInput
    }


    private void HandleMovementInput() 
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        if(movementInput.x < -LeftStickGap2 || movementInput.x > LeftStickGap2 || movementInput.y < -LeftStickGap2 || movementInput.y > LeftStickGap2) 
        {
            Debug.Log("Its working");
            animator.SetInteger("Whatever", 5);
        }
        else if (movementInput.x < -LeftStickGap || movementInput.x > LeftStickGap || movementInput.y < -LeftStickGap || movementInput.y > LeftStickGap)
        {
            animator.SetInteger("Whatever", 2);
        }
        else if(!attackInput)
        {
            animator.SetInteger("Whatever", 1);
        }


    }

    private void HandleJumpingInput() 
    {
        if (jump_input) 
        {
            Debug.Log("Button works");
            Jumping();
        }
    }
}
