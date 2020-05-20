using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModestoFlameThrower : MonoBehaviour
{
    public enum FlameState { Ready = 0, Charging = 1, Channelling, CoolDown = 3 }
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
                if (Input.GetAxis("Ability 1") > 0)
                    CastAbility();
                break;

            //Handles Charging ability
            case FlameState.Charging:
                ChargeAbility();
                break;

            //Creates fire while channelling
            case FlameState.Channelling:
                ChannelAbility();
                break;

            //Handles Ability Cooldown
            case FlameState.CoolDown:
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
        FlameDeltaRemaining = 0;

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
            Debug.Log("Charging: " + ChargeTimeRemaining);
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
        if (DurationRemaining > 0 /*|| Input.GetAxis("Primary Fire") > 0*/)
        {
            Debug.Log("Channelling: " + DurationRemaining);
            //Spawn a new projectile every FlameDelta seconds
            if (FlameDeltaRemaining > 0)
            {
                FlameDeltaRemaining -= Time.deltaTime;
            }
            else
            {
                CreateFlames();
                FlameDeltaRemaining = FlameDelta;
            }

            DurationRemaining -= Time.deltaTime;
        }
        else
        {
            DurationRemaining = 0;
            Status = FlameState.CoolDown;
        }
    }

    //Handles Ability Cooldown
    void AbilityCoolDown()
    {
        //wait for cooldown to finish
        if (CoolDownRemaining > 0)
        {
            Debug.Log("CoolDown: " + CoolDownRemaining);
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
