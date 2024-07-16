using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    Vector3 PlayerVelocity;

    // Start is called before the first frame update
    [SerializeField] float movementSpeed = 7;
    [SerializeField] float jumpForce = 5;


    [SerializeField] LayerMask Ground;
    [SerializeField] Transform GroundCheck;


    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 move = new Vector3 (HorizontalInput, 0, VerticalInput);
        controller.Move(move * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            Jump();
        }

        
        if (true)
        {
            gameObject.transform.position = move;
        }
        
    }

    void Jump()
    {
       
    }


}
