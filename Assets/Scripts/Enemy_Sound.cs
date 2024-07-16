using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sound : MonoBehaviour
{
    [SerializeField] AudioSource Destroyed;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroyed.Play();
        }
    }
}
