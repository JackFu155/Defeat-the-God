using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModestoFireDash : MonoBehaviour
{
    public enum CastState { Ready = 0, Charging = 1, Channelling = 2, CoolDown = 3 }
    public CastState Status;

    public float CoolDown;
    public float Duration;
    public float ChargeTime;
    public float FlameSpotDelta; //Time between flame objects

    public GameObject FlameSpotPrefab;
    public Transform Player;

    private float CoolDownRemaining;
    private float DurationRemaining;
    private float ChargeTimeRemaining;
    private float FlameSpotDeltaRemaining;

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
                if (Input.GetAxis("Ability 1") > 0)
                    CastAbility();
                break;

            //Handles Charging ability
            case CastState.Charging:
                ChargeAbility();
                break;

            //Creates fire while channelling
            case CastState.Channelling:
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

    private void ChargeAbility()
    {
        throw new NotImplementedException();
    }

    private void ChannelAbility()
    {
        throw new NotImplementedException();
    }

    private void AbilityCoolDown()
    {
        throw new NotImplementedException();
    }
}
