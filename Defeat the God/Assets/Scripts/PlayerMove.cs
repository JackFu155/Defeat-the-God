using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //x, z = horizontal
    //y = vertical

    private Vector3 velocity;
    private Vector3 prevVelocity;
    private Vector3 acceleration;
    public float walkSpeed = 2f;
    public float accelerateSpeed = 2f;
    public float jumpPower = 10f;
    public float maxFallSpeed = 10f;

    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);
        acceleration = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Set acceleration to 0 before calculations
        acceleration = new Vector3(0f, 0f, 0f);
        velocity = gameObject.GetComponent<Rigidbody>().velocity;

        //Movement based on Velocity
        if (Math.Abs(Input.GetAxis("Horizontal")) > .1f)
        {
            acceleration.x = Input.GetAxis("Vertical") * accelerateSpeed;
        }
        else if(Math.Abs(velocity.x) > 0)
        {
            if (Math.Abs(velocity.x) < .1f)
                velocity.x = 0;
            else
                velocity.x /= 2f * Time.deltaTime;
        }

        if (Math.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            acceleration.z = Input.GetAxis("Vertical") * accelerateSpeed;
        }
        else if (Math.Abs(velocity.z) > 0)
        {
            if (Math.Abs(velocity.z) < .1f)
                velocity.z = 0;
            else
                velocity.z /= 2f * Time.deltaTime;
        }

        //Determine if player is able to jump
        canJump = false;
        if (true) //TODO: Add logic
        {
            canJump = true;
        }

        //Jump
        if (Input.GetAxis("Jump") > 0f && canJump)
        {

            velocity.y = jumpPower;
            canJump = false;
        }

        //Constraint calculations
        velocity += acceleration * Time.deltaTime;

        //Clamp x and z movement
        Vector2 horizontal = new Vector2(velocity.x, velocity.z); //Horizontal's y component is the z component of velocity
        Vector2.ClampMagnitude(horizontal, walkSpeed);
        velocity.x = horizontal.x;
        velocity.z = horizontal.y;

        //Clamp vertical movement
        if (-velocity.y > maxFallSpeed)
        {
            velocity.y = -maxFallSpeed;
        }

        //Set velocities and acceleration
        gameObject.GetComponent<Rigidbody>().velocity = velocity;
        prevVelocity = velocity;
    }
}
