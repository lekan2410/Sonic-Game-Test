using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] AudioSource RingSource;
    [SerializeField] AudioSource EmeraldSource;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ring"))
        {
            Destroy(other.gameObject);
            RingSource.Play();
        }

        else if (other.gameObject.CompareTag("Emerald"))
        {
            Destroy(other.gameObject);
            EmeraldSource.Play();
        }
    }
}
