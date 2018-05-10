using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    public float spawnDelay = 3.0f;
    private float setSpawnDelay;

    float DT;

    public GameObject spawnedEnemy;
    public GameObject AITarget;

    void doSpawn()
    {
        GameObject temp = Instantiate(spawnedEnemy, transform.position, transform.rotation);

        temp.GetComponent<AIController>().TargetPoint = AITarget;
    }



	// Use this for initialization
	void Start () {
        AITarget = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        DT = Time.deltaTime;

        setSpawnDelay -= DT;

        if (setSpawnDelay <= 0)
        {
            doSpawn();
            setSpawnDelay = spawnDelay;
        }

	}
}
