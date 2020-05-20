using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerProjectile : MonoBehaviour
{
    public float LifeTime;
    private float TimeAlive;
    private Vector3 OriginalSize;

    public float Damage;
    public float Speed;

    private void Start()
    {
        TimeAlive = 0;
        OriginalSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Grow();
        MoveForward();

        if(TimeAlive >= LifeTime)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            TimeAlive += Time.deltaTime;
        }
    }

    //Grows at a constant rate finishing at full size
    void Grow()
    {
        float scale = TimeAlive / LifeTime;

        if (scale == 0)
            scale = 0.001f;
        transform.localScale = new Vector3(scale * OriginalSize.x, scale * OriginalSize.y, 1 * OriginalSize.z);
    }

    //Moves forward at a constant rate
    void MoveForward()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
