using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] AudioSource Thorn;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -1)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Thorngrind"))
        {
            rb.velocity = new Vector3(10, 5, rb.velocity.z);
            Thorn.Play();
        }
    }

    void Die()
    {
        Invoke(nameof(ReloadLevel), 1.3f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   

    
}

