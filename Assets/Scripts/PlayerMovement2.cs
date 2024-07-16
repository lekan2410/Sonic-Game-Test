using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement2 : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform cam;
    public LayerMask layerMask;

    Vector3 PlayerPosition;

    public Rigidbody rb;

    [SerializeField] float groundDistance; 


    [SerializeField] AudioSource Jumping_Sound;
    [SerializeField] AudioSource Jumping_Voice;
    [SerializeField] AudioSource Joy_Voice;
    [SerializeField] AudioSource boostingSound;
    [SerializeField] AudioSource brake_sound;
    [SerializeField] AudioSource Rainbow_Ring_Sound;

    

    public Transform orientation;

    public float dashpanel_speed = 4;

    public float rotateSpeed;

    private float movementSpeed = 30;
    private float jumpForce = 10;

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    bool grounded;

    void Start()
    {
       
    }

    // Update is called once per frame
     void FixedUpdate()
    {
        IsGrounded();

        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");

        Vector3 MoveDirection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;


        if (Input.GetButton("Jump") && grounded)
        {
            Jump();
            Jumping_Sound.Play();
            Jumping_Voice.Play();
            jumpForce = 10;
        }
        
         else if (!grounded && Input.GetKey(KeyCode.C))
        {
            Jump();
            jumpForce = -40;
        }

        else if (grounded && Input.GetKeyDown(KeyCode.C))
        {
            movementSpeed = 0;
        }

        else if (Input.GetKey(KeyCode.C))
        {
            brake_sound.Play();
        }

        else if (grounded && !Input.GetKey(KeyCode.C) && !Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = 30;
        }


        else if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Joy_Voice.Play();
        }


        if (grounded)
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        boosting();

        rb.AddForce(MoveDirection.normalized * movementSpeed, ForceMode.Force);

       
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spring"))
        {
            jumpForce = 25;
            Jump();
            jumpForce = 10;
        }

        else if (other.gameObject.CompareTag("WideSpring"))
        {
            jumpForce = 90;
            Jump();
            jumpForce = 10;
        }


        else if (other.gameObject.CompareTag("Dashpanel"))
        {
            rb.velocity = transform.forward;
            movementSpeed = 300;
        }

        else if (other.gameObject.CompareTag("Enemy_Target"))
        {
            Jump();
        }

        else if (other.gameObject.CompareTag("Rainbow_Ring") && rb.velocity.x <= 0)
        {
            Rainbow_Ring_Sound.Play();
            rb.velocity = new Vector3(rb.velocity.x - 20, 0, rb.velocity.z);
        }

        else if (other.gameObject.CompareTag("Rainbow_Ring") && rb.velocity.x >= 0)
        {
            Rainbow_Ring_Sound.Play();
            rb.velocity = new Vector3(rb.velocity.x + 20, 0, rb.velocity.z);
        }

       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Dashpanel"))
        {
            movementSpeed = 200;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.y);
    }

    void boosting()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 70;
        }

        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 30;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            boostingSound.Play();
        }


    }

    void IsGrounded()
    {
        grounded = Physics.Raycast(rb.worldCenterOfMass, -rb.transform.up, out RaycastHit hit, groundDistance, layerMask, QueryTriggerInteraction.Ignore);
    }


}
