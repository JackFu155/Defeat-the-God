using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpot : MonoBehaviour
{
    public float LifeTimeLeft; //Inherited from FlameSpotProjectile
    public float DamagePerSecond;
    public float TickTime; //The time between Damages

    // Update is called once per frame
    void Update()
    {
        if(LifeTimeLeft > 0.0f)
        {
            LifeTimeLeft -= Time.deltaTime;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
