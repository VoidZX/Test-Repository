using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour {

    public Image HealthBar;

    public HealthComponent PlayerHealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HealthBar.fillAmount = PlayerHealth.currentHealth / PlayerHealth.maxHealth;
	}
}
