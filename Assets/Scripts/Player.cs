using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private CharacterController characterController;
    private PlayerInput playerInput;
    private Animator animator;

    private int a_isWalking;
    private int a_isRunning;


    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunningPressed;
    float rotationFactorPerFrame = 10f;

    [SerializeField] private float velocity;
    [SerializeField] private float runMultiplierVelovity = 3;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        GetAnimatorParameters();


        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;

        playerInput.CharacterControls.Run.started += OnRunningInput;
        playerInput.CharacterControls.Run.canceled += OnRunningInput;
    }

    private void GetAnimatorParameters()
    {
        a_isWalking = Animator.StringToHash("isWalking");
        a_isRunning = Animator.StringToHash("isRunning");
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplierVelovity;
        currentRunMovement.z = currentMovementInput.y * runMultiplierVelovity;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void OnRunningInput(InputAction.CallbackContext context)
    {
        isRunningPressed = context.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        AnimationHandler();
        RotationHandler();
    }

    private void RotationHandler()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        Debug.Log(isMovementPressed);
        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void AnimationHandler()
    {
        bool isWalkingAnimation = animator.GetBool(a_isWalking);
        bool isRunningAnimation = animator.GetBool(a_isRunning);

        if (isMovementPressed && !isWalkingAnimation)
        {
            animator.SetBool(a_isWalking, true);
        }
        else if (!isMovementPressed && isWalkingAnimation)
        {
            animator.SetBool(a_isWalking, false);
        }

        if (isMovementPressed && isRunningPressed && !isRunningAnimation)
        {
            animator.SetBool(a_isRunning, true);
        }
        else if (!isMovementPressed || !isRunningPressed && isRunningAnimation)
        {
            animator.SetBool(a_isRunning, false);
        }
    }

    private void MovePlayer()
    {
        if (isRunningPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime * velocity);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime * velocity);
        }
    }




    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
