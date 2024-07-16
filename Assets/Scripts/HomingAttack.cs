using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HomingAttack : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float groundDistance;

    public LayerMask layerMask;

    public GameObject[] AllObjects;
    public GameObject[] EnemyObjects;
    public GameObject NearestOBJ;

    [SerializeField] AudioSource Destroyed;

    private GameObject EmptyGameObject;

    public float distance;
    public float distance2;
    public float nearestDistance = 13;

    bool inrange = false;
    bool inair = false;

    bool grounded;


    private void FixedUpdate()
    {
        IsGrounded();
        targets();
    }
    

    void targets()
    {
        bool jumping = Input.GetKey("q");

        AllObjects = GameObject.FindGameObjectsWithTag("Target");

        

        for (int i = 0; i < AllObjects.Length; i++)
        {
            float distance = Vector3.Distance(rb.transform.position, AllObjects[i].transform.position);


            if (distance <= nearestDistance)
            {
                NearestOBJ = AllObjects[i];
                nearestDistance = distance;
            }

            
        }

        distance2 = Vector3.Distance(rb.transform.position, NearestOBJ.transform.position);

        if (distance2 <= 10)
        {
            inrange = true;
        }

        else if (distance2 >= 10)
        {
            inrange = false;
        }

        if (!grounded && jumping && !inair && inrange)
        {
            rb.transform.position = NearestOBJ.transform.position;
        }

        else if (!grounded && jumping)
        {
            rb.transform.position = rb.transform.position;
        }

        if (grounded)
        {
            inair = false;
        }

        nearestDistance = 1000;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spring"))
        {
            inair = true;
        }

        if (other.gameObject.CompareTag("Enemy_Target"))
        {
            Destroyed.Play();
            Destroy(other.gameObject);

            
        }
    }

    void IsGrounded()
    {
        grounded = Physics.Raycast(rb.worldCenterOfMass, -rb.transform.up, out RaycastHit hit, groundDistance, layerMask, QueryTriggerInteraction.Ignore);
    }
}
