using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{

    public float Speed;
    public float JumpForce;
    public Transform Foot; // Transform at the player's feet

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        // Get keyboard inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Jump");

        // Apply movement to game object
        if (y != 0 && CanJump()) // If the jump key is pressed
        {
            gameObject.GetComponent<Rigidbody>().velocity = (((transform.forward * z) + (transform.right * x)) * Speed) + (new Vector3(0, JumpForce, 0));
        }
        else // Don't add jump velocity
        {
            gameObject.GetComponent<Rigidbody>().velocity = ((transform.forward * z) + (transform.right * x)) * Speed + (new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0));
        }
    }

    private bool CanJump()
    {
        // Perform a raycast to determine if there is floor below the player
        RaycastHit hit;

        return Physics.Raycast(Foot.position, -Foot.up, out hit, 0.1f);
    }
}
