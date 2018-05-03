using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamage : MonoBehaviour {

    public float DamageAmount;

    public void OnTriggerEnter (Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            other.GetComponent<HealthComponent>().takeDamage(DamageAmount);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
