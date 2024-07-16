using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCAM : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform orientation;
    public Transform playerOBJ;
    public Transform player;
    public Rigidbody rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        Vector3 ViewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = ViewDir.normalized;

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 InputDir = orientation.forward * Vertical + orientation.right * Horizontal;
        
       if (InputDir != Vector3.zero)
        {
            playerOBJ.forward = Vector3.Slerp(playerOBJ.forward, InputDir, Time.deltaTime);
        }
    }

}
