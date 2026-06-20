using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float gravity = -7f;
    private CharacterController characterController;

    private Vector3 inputVector = new();
    private Vector3 movementVector = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello world... It's a great day today for shredding paper.");
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    public void OnMove(InputValue value) {
        Vector2 moveInput = value.Get<Vector2>();
        inputVector.x = moveInput.x;
        inputVector.z = moveInput.y;
        inputVector.Normalize();
    }


    private void MovePlayer()
    {
        movementVector = inputVector * speed + (gravity * Vector3.up);
        characterController.Move(movementVector * Time.deltaTime);
    }
}
