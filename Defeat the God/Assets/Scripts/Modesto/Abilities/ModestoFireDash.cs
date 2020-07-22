using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModestoFireDash : MonoBehaviour
{
    public enum CastState { Ready = 0, Charging = 1, Channeling = 2, CoolDown = 3 }
    public CastState Status;

    public float CoolDown;
    public float Duration;
    public float ChargeTime;
    public float FlameSpotDelta; //Time between flame objects
    public float DashDistance; //The max distance of the dash

    public GameObject FlameSpotProjectilePrefab;
    public Transform Player;

    private float CoolDownRemaining;
    private float DurationRemaining;
    private float ChargeTimeRemaining;
    private float FlameSpotDeltaRemaining;

    private Vector3 DashDirection;

    private bool hasDashed = false;

    // Start is called before the first frame update
    void Start()
    {
        Status = CastState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Status)
        {
            //Waits for ability activation
            case CastState.Ready:
                if (Input.GetAxis("Ability 2") > 0)
                    CastAbility();
                break;

            //Handles Charging ability
            case CastState.Charging:
                ChargeAbility();
                break;

            //Creates fire while channeling
            case CastState.Channeling:
                ChannelAbility();
                break;

            //Handles Ability Cooldown
            case CastState.CoolDown:
                AbilityCoolDown();
                break;
        }
    }

    //Sets/Preps defaults for casting ability
    void CastAbility()
    {
        Debug.Log("Casting: ");
        //Set values to prep for casting
        ChargeTimeRemaining = ChargeTime;
        DurationRemaining = Duration;
        FlameSpotDeltaRemaining = 0;
        CoolDownRemaining = CoolDown;

        //Change status
        Status = CastState.Charging;
    }

    //No charge time so set straight to channeling
    private void ChargeAbility()
    {
        ChargeTimeRemaining = 0;
        Status = CastState.Channeling;
        hasDashed = false;
    }

    //Handles moving the player and drops firespots
    private void ChannelAbility()
    {
        //Dash at the beginning of the cast
        if(!hasDashed)
        {
            Dash();
        }

        //While casting duration is available
        if (DurationRemaining > 0)
        {
            if (FlameSpotDeltaRemaining > 0)
            {
                FlameSpotDeltaRemaining -= Time.deltaTime;
            }
            else
            {
                DropFire();
                FlameSpotDeltaRemaining = FlameSpotDelta;
            }

            DurationRemaining -= Time.deltaTime;
        }
        else //Put ability on cooldown
        {
            DurationRemaining = 0;
            Status = CastState.CoolDown;
        }

    }

    //Handles Ability Cooldown
    private void AbilityCoolDown()
    {
        //Wait for cooldown to finish
        if (CoolDownRemaining > 0)
        {
            Debug.Log("CoolDown: " + CoolDownRemaining);
            CoolDownRemaining -= Time.deltaTime;
        }
        else
        {
            CoolDownRemaining = 0;
            Status = CastState.Ready;
        }
    }

    //Handles dashing
    private void Dash()
    {
        if (DurationRemaining > 0)
        {
            //Calculate the dash direction on the first frame of casting
            if (DurationRemaining == Duration)
            {
                //Get current direction of player
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");
                DashDirection = transform.right * x + transform.forward * z;

                //Dash forward if player is not moving in a direction
                if (DashDirection.magnitude == 0)
                    DashDirection = transform.forward;

                //Normalize the direction and multiply by the dash power
                DashDirection.Normalize();
                DashDirection *= DashDistance;
            }

            //Add force to player
            gameObject.GetComponent<Rigidbody>().AddForce(DashDirection * DashDistance / Duration);
            //gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity + DashDirection;
        }
        else
        {
            hasDashed = true;
        }
    }

    //Drops a firespot projectile at the player's feet
    private void DropFire()
    {
        GameObject flameSpot = Instantiate(FlameSpotProjectilePrefab, Player.position - new Vector3(0, 0.75f, 0), Player.rotation);
        flameSpot.GetComponent<FlameSpotProjectile>();
    }
}
