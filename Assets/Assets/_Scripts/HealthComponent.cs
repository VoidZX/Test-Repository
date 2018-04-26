using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {

    // objectHealth = otherScriptOnObject.startingHealth  // This is the ideal in psuedocode
    public float maxHealth = 100;
    public float currentHealth;
    

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
    }

	public void takeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
    }

	// Update is called once per frame
	void Update () {

    //if (currentHealth >> maxHealth)

    if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    else
        {
            gameObject.SetActive(true);
        }
		
	}
}
