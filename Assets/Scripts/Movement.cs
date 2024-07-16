using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animator;
   
    bool springjump;
    bool falling;
    bool Dashpanel_boost;
    bool stomping;
    bool Jumping;
    bool damage1;
    bool hit_enemy;
    bool ring_animation;

    public Rigidbody rb;


    [SerializeField] float groundDistance;
    public LayerMask layerMask;

    bool grounded;


    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded();

        bool Jumping = Input.GetButton("Jump");
        bool boosting = Input.GetKey("left shift");
        bool stomping = Input.GetKey("c");
        bool homing_attack = Input.GetKey("d");


        // Checking if Player is Jumping

        if(Jumping) animator.SetBool("IsJumping", true);
        else if (!Jumping && grounded) animator.SetBool("IsJumping", false);

        // Checking if Player is boosting
        if (boosting) animator.SetBool("IsBoosting", true);
        else if (!boosting) animator.SetBool("IsBoosting", false);

        // Checking if Player is boosting in the air 
        if (boosting && !grounded) animator.SetBool("AirBoosting", true);
        else if (grounded) animator.SetBool("AirBoosting", false);

        
        
        // Checking if Player is Crouching
        if (stomping && !grounded)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsStomping", true);
            animator.SetBool("Falling", false);
            animator.SetBool("HitEnemy", false);
        }
        else if (grounded && !stomping) animator.SetBool("IsStomping", false);

        if (grounded && stomping) animator.SetBool("Stop", true);
        else if (grounded && !stomping) animator.SetBool("Stop", false);


        if (springjump) animator.SetBool("SpringJump", true);
        else if (!springjump) animator.SetBool("SpringJump", false);

        if (falling) animator.SetBool("Falling", true);
        else if (!falling) animator.SetBool("Falling", false);

        if (Dashpanel_boost) animator.SetBool("Dashpanel_boost", true);
        else if (!Dashpanel_boost) animator.SetBool("Dashpanel_boost", false);


        if(transform.position.y <= -0.3)
        {
            animator.SetBool("DeathFalling", true);
        }

        else if (transform.position.y >= 0)
        {
            animator.SetBool("DeathFalling", false);

        }

        if (hit_enemy && !grounded && homing_attack)
        {
            Jumping = false;
            animator.SetBool("HitEnemy", true);
        }
         
        else if (!hit_enemy) animator.SetBool("HitEnemy", false);

        

        damage();
        Quickstep();
        run();
        boosting_direction();
        jumping();
        ring_dash();
    }

    void ring_dash()
    {
        // Setting animation when sonic touches the large ring
        if (ring_animation && !grounded) animator.SetBool("Ring_animation", true);
        else if (grounded) animator.SetBool("Ring_animation", false);
    }

    void damage()
    {
        if (damage1) animator.SetBool("Damage1", true);
        else if (!damage1) animator.SetBool("Damage1", false);    
    }

    void Quickstep()
    {
        bool drifting_left = Input.GetKey("q");
        bool drifting_right = Input.GetKey("e");

        if (drifting_right) animator.SetBool("IsDrifting_r", true);
        if (!drifting_right) animator.SetBool("IsDrifting_r", false);

        if (drifting_left) animator.SetBool("IsDrifting_l", true);
        if (!drifting_left) animator.SetBool("IsDrifting_l", false);
    }

    void run()
    {
        bool running = Input.GetKey("w");
        bool running_backwards = Input.GetKey("s");
        bool running_left = Input.GetKey("a");
        bool running_right = Input.GetKey("d");

        bool running_left2 = Input.GetKey("q");
        bool running_right2 = Input.GetKey("e");

        if (running) animator.SetBool("IsRunning", true);
        else if (!running) animator.SetBool("IsRunning", false);

        if (running_backwards) animator.SetBool("IsTurning", true);
        else if (!running_backwards) animator.SetBool("IsTurning", false);

        if (running_left || running_left2) animator.SetBool("Run_left", true);
        else if (!running_left || running_left2) animator.SetBool("Run_left", false);

        if (running_right || running_right2) animator.SetBool("Run_right", true);
        else if (!running_right || running_right2) animator.SetBool("Run_right", false);


    }

    void boosting_direction()
    {
        bool boosting_left = Input.GetKey("a");
        bool boosting_right = Input.GetKey("d");

        if (boosting_left) animator.SetBool("Boost_Left", true);
        if (!boosting_left) animator.SetBool("Boost_Left", false);

        if (boosting_right) animator.SetBool("Boost_Right", true);
        if (!boosting_right) animator.SetBool("Boost_Right", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spring") || (other.gameObject.CompareTag("WideSpring")))
        {
            springjump = true;
            falling = false;

        }

        else if (other.gameObject.CompareTag("Dashpanel"))
        {
            Dashpanel_boost = true;
        }

        else if (other.gameObject.CompareTag("Enemy_Target"))
        {
            hit_enemy = true;
            animator.Play("homing_act1", -1, 0);

        }

        else if (other.gameObject.CompareTag("Rainbow_Ring"))
        {
            ring_animation = true;
            springjump = false;
            falling = false;
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Spring") || other.gameObject.CompareTag("WideSpring"))
        {

            falling = true;
            springjump = false;

        }   
        

        else if (other.gameObject.CompareTag("Dashpanel"))
        {
            Dashpanel_boost = false;
        }

        if (other.gameObject.CompareTag("Thorngrind"))
        {
            damage1 = true;
        }


    }

    void jumping()
    {
        if (grounded)
        {
            springjump = false;
            falling = false;
            damage1 = false;
            hit_enemy = false;
            ring_animation = false;
        }
       
    }

    void IsGrounded()
    {
         grounded = Physics.Raycast(rb.worldCenterOfMass, -rb.transform.up, out RaycastHit hit, groundDistance, layerMask, QueryTriggerInteraction.Ignore);
    }


}
