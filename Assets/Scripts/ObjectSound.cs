using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    [SerializeField] AudioSource Sound;
    Animator animator;
    bool jumping;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jumping = true;
            Sound.Play();
            
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jumping = false;
        }
    }

    private void Update()
    {
        if (jumping) animator.SetBool("IsAnimating", true);
        else if (!jumping) animator.SetBool("IsAnimating", false);

    }

}
