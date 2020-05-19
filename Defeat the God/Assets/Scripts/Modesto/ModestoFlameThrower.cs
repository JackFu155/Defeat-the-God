using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModestoFlameThrower : MonoBehaviour
{
    public enum FlameState { Ready = 0, Charging = 1, Channelling,  CoolDown = 3 }
    public FlameState Status;
    public float CoolDown;
    public float Duration;
    public float ChargeTime;
    public float FlameDelta; //Time between flame objects

    public GameObject FlamePrefab;
    public Transform target;

    private float CoolDownRemaining;
    private float DurationRemaining;
    private float ChargeTimeRemaining;
    private float FlameDeltaRemaining;

    // Start is called before the first frame update
    void Start()
    {
        Status = FlameState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Status)
        {
            //Waits for ability activation
            case FlameState.Ready:
                if(Input.GetAxis("Ability 1") > 0)
                    CastAbility();
                break;

            //Handles Charging ability
            case FlameState.Charging:
                ChargeAbility();
                break;

            //Creates fire while channelling
            case FlameState.Channelling:
                break;

            //Handles Ability Cooldown
            case FlameState.CoolDown:
                break;
        }
    }

    //Sets/Preps defaults for casting ability
    void CastAbility()
    {
        //Set values to prep for casting
        ChargeTimeRemaining = ChargeTime;
        DurationRemaining = Duration;
        FlameDeltaRemaining = FlameDelta;

        //Change status
        Status = FlameState.Charging;
    }

    //Handles Charging ability
    void ChargeAbility()
    {
        //disable main attack
        //gameObject.GetComponent<ModestoAttack>().enabled = false;

        //wait for animation
        if (ChargeTimeRemaining > 0)
        {
            ChargeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            ChargeTimeRemaining = 0;
            Status = FlameState.Channelling;
        }
    }

    //Creates fire while channelling
    void ChannelAbility()
    {
        //Cast the ability unless canceled
        if (ChargeTimeRemaining > 0 /*|| Input.GetAxis("Primary Fire") > 0*/)
        {
            if (FlameDeltaRemaining > 0)
            {
                FlameDeltaRemaining -= Time.deltaTime;
            }
            else
            {
                CreateFlames();
                FlameDeltaRemaining = FlameDelta;
            }
            
            ChargeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            ChargeTimeRemaining = 0;
            Status = FlameState.Channelling;
        }
    }

    //Handles Ability Cooldown
    void AbilityCoolDown()
    {
        //wait for cooldown to finish
        if (CoolDownRemaining > 0)
        {
            CoolDownRemaining -= Time.deltaTime;
        }
        else
        {
            CoolDownRemaining = 0;
            Status = FlameState.Ready;
        }
    }

    //Creates flames
    void CreateFlames()
    {
        //TODO: Create Flame prefab
        Instantiate(FlamePrefab, target.position, target.rotation);
    }
}
