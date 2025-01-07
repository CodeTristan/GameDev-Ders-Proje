using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddOn : MonoBehaviour
{
    public int damage;
    private Rigidbody rb;
    private bool targetHit;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //destroy the object after 3s even if it doesnt hit anything
        Destroy(this.gameObject, 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //make sure to only hit the first target you collide
        if(targetHit) return;
        else targetHit = true;

        //check if you hit an enemy
        if(collision.gameObject.GetComponent<BasicEnemy>() != null)
        {
            BasicEnemy enemy = collision.gameObject.GetComponent<BasicEnemy>();
            enemy.TakeDamage(damage); 
        }

        //destroy the object when you hit
        Destroy(this.gameObject);
    }

    void Update()
    {

    }
}
