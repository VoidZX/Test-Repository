using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float DamageAmount = 10; // How much damage the bullet does
    public float BulletSpeed = 50; // Velocity of bullet
    public float BulletDelay = .2f; // How often
    public GameObject Owner;

    public void OnTriggerEnter(Collider other) //other == The object we collide with.
    {

        if (other.gameObject != Owner)
        {
            HealthComponent Temp = other.GetComponent<HealthComponent>();
            if (Temp != null)
            {
                Temp.takeDamage(DamageAmount);
            }
        }

    }
}
