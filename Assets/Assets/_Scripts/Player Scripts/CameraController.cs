using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class CameraController : MonoBehaviour {

//    enum States
//    {
//        PlayerCam,
//        ObjectCam,
//        PostUpInRoom
//    }

//    public float CameraSpeed = 1;

//    public GameObject Pillar;

//    public PlayerController Player;

//    public Vector3 PlayerOffset;

//    public Vector3 PillarOffset;

//    States CurrentState;

//    void CameraMove(Vector3 Location)
//    {
//        transform.position = Vector3.MoveTowards(transform.position, Location, CameraSpeed);
//    }

//    void CameraShowcase(Vector3 PillarLocation)
//    {
//        transform.position = Vector3.MoveTowards(transform.position, PillarLocation, CameraSpeed);
//    }

//    // Use this for initialization
//    void Start () {
//        PlayerOffset = transform.position - Player.transform.position;

//        PillarOffset = transform.position - Pillar.transform.position;
//    }
	
//	// Update is called once per frame
//	void Update () {



//        if (Input.GetKey(KeyCode.Q))
//        {
//            CurrentState = ObjectCam;
//        }
//        else
//        {
//            CurrentState = PlayerCam;
//        }


//    }

//    private void FixedUpdate()
//    {
//        switch (CurrentState)
//        {
//            case States.PlayerCam:
//                CameraMove(Player.transform.position + PlayerOffset);
//                break;
//            case States.ObjectCam:
//                CameraShowcase(Pillar.transform.position + PillarOffset);
//                break;
//            case States.PostUpInRoom:
//                break;
//        }
//    }
//}
