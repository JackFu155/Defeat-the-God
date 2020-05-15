using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 prevVelocity;
    public float walkSpeed = 2f;
    //public float jumpPower = 10f;
    private Vector3 position;

    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement based on Velocity

        //Left & Right
        if (Math.Abs(Input.GetAxis("Horizontal")) > .1f)
        {
            velocity.x = Input.GetAxis("Horizontal") * walkSpeed;
        }
        else
        {
            velocity.x = 0f;
        }

        //Forward and Backward
        if (Math.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            velocity.z = Input.GetAxis("Vertical") * walkSpeed;
        }
        else
        {
            velocity.z = 0f;
        }

        //Reset Jump
        if (prevVelocity.z < 0 && velocity.z > prevVelocity.z)
        {
            canJump = true;
        }

        //Jump
        /*if (Input.GetAxis("Jump") > 0f && canJump)
        {
            velocity.y = jumpPower;
            canJump = false;
        }
        else
        {
            velocity.y = 0f;
        }*/

        //Set transform position
        position += velocity * Time.deltaTime;
        transform.position = position;


        //Set previous values for next Update()
        prevVelocity = velocity;
        UnityEngine.Debug.Log(velocity.ToString());

        //velocity.y = gameObject.getComponent<RigidBody>().velocity.y;
    }
}
