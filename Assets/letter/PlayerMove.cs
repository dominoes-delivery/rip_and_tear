using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float gravity = -7f;
    [SerializeField] private float friction = 1f;
    [Tooltip("Mouse Sensitivity")]
    [SerializeField] private float skibidibopOhioRizzlerGyatt = 1f;
    private CharacterController characterController;

    private Vector3 inputVector = new();
    private Vector3 movementVector = new();

    [SerializeField] private Animator camAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello world... It's a great day today for shredding paper.");
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        looksmaxxing();
    }

    public void OnMove(InputValue value) {
        Vector2 moveInput = value.Get<Vector2>();
        inputVector.x = moveInput.x;
        inputVector.z = moveInput.y;
        inputVector.Normalize();
    }


    private void MovePlayer()
    {
        //Debug.Log("inputVector: " + inputVector.ToString());
        if (inputVector.magnitude == 0f)
        {
            //Debug.Log("movementVector: " + movementVector.ToString());
            movementVector.x = Mathf.MoveTowards(movementVector.x, 0, friction * Time.deltaTime);
            movementVector.z = Mathf.MoveTowards(movementVector.z, 0, friction * Time.deltaTime);
            camAnim.SetBool("deeez nuts me movin", false);
        }
        else
        {
            movementVector = transform.TransformDirection(inputVector) * speed + (gravity * Vector3.up);
            camAnim.SetBool("deeez nuts me movin", true);
        }
        characterController.Move(movementVector * Time.deltaTime);
    }
    private void looksmaxxing()
    {
        look += input_yo_looks_man;
        //Debug.Log("Look: " + look.ToString());
        transform.localRotation = Quaternion.AngleAxis(look, transform.up);
    }

    private float look;
    private float input_yo_looks_man = 0;
    public void OnLook(InputValue value)
    {
        Vector2 deeznutsInput = value.Get<Vector2>();
        input_yo_looks_man = deeznutsInput.x * skibidibopOhioRizzlerGyatt;
    }
}
