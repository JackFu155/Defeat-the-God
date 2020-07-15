using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpotProjectile : MonoBehaviour
{
    public float LifeTimeLeft;
    public float DamagePerSecond;
    public float TickTime;
    public GameObject FlameSpotPrefab;

    // Update is called once per frame
    void Update()
    {
        if (LifeTimeLeft > 0.0f)
        {
            LifeTimeLeft -= Time.deltaTime;

            //Fall at 5 m/s
            transform.Translate(Vector3.down * 5 * Time.deltaTime);

            //On hitting the ground
            if(CheckCollision())
            {
                // Create a new flamespot and transfer variables
                GameObject flameSpot = Instantiate(FlameSpotPrefab, gameObject.transform.position, gameObject.transform.rotation);

                flameSpot.GetComponent<FlameSpot>().LifeTimeLeft = LifeTimeLeft;
                flameSpot.GetComponent<FlameSpot>().DamagePerSecond = DamagePerSecond;
                flameSpot.GetComponent<FlameSpot>().TickTime = TickTime;

                GameObject.Destroy(gameObject);
            }
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    //Check to see if it has hit the ground
    private bool CheckCollision()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.05f))
        {
            //Returns false if hitting a player
            return !hit.collider.gameObject.CompareTag("Player");           
        }
        else
        {
            //Returns false if not hitting anything
            return false;
        }
    }
}
