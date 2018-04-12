using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    enum States
    {
        MovementState,
        MovementStateSprint,
        CombatState
    }

    private Rigidbody RB;
    public Animator Anim; 
    public float MovementSpeed = 100;

    public float SprintModifier = 2.5f;

    public GameObject Cam;

    public Vector3 IP; // Movement Input

    States CurrentState;

    float DT;

	// Use this for initialization
	void Start () {

        RB = GetComponent<Rigidbody>(); //Gets Rigidbody on object
        // anim passed in through editor
	}
	
    public void KeyInput()
    {
        IP.x = Input.GetAxisRaw("Horizontal");
        IP.z = Input.GetAxisRaw("Vertical");
    }

    public void doMovement(float DeltaTime, Vector3 MoveInput, Transform DirTrans)
    {
        if (RB != null)
        {
            Vector3 Forward = DirTrans.forward * IP.z;
            Forward.y = 0;
            Forward.Normalize();

            Vector3 Right = DirTrans.right * IP.x;

            RB.AddForce(Forward * MovementSpeed * DeltaTime);
            RB.AddForce(Right * MovementSpeed * DeltaTime);
        }
    }

    public void doMovementSprint(float DeltaTime, Vector3 MoveInput, Transform DirTrans)
    {
        if (RB != null)
        {
            Vector3 Forward = DirTrans.forward * IP.z;
            Forward.y = 0;
            Forward.Normalize();

            Vector3 Right = DirTrans.right * IP.x;

            RB.AddForce(Forward * MovementSpeed * DeltaTime * SprintModifier);
            RB.AddForce(Right * MovementSpeed * DeltaTime * SprintModifier);
        }
    }

    public void updateAnim(Animator AnimationController)
    {
        if (AnimationController != null)
        {
            var localVel = transform.InverseTransformDirection(RB.velocity);
            AnimationController.SetFloat("ForwardSpeed", localVel.z);
            AnimationController.SetFloat("RightSpeed", localVel.x);
        }
    }

    public void doMovementState()
    {
        doMovement(DT,IP, Cam.transform);
    }

    public void doMovementStateSprint()
    {
        doMovementSprint(DT, IP, Cam.transform);
    }

    public void doCombatState()
    {

    }

    // Update is called once per frame
    void Update () {

        DT = Time.deltaTime;

        KeyInput();

        updateAnim(Anim);


        if (CurrentState != States.CombatState)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                CurrentState = States.MovementStateSprint;
            }
            else
            {
                CurrentState = States.MovementState;
            }
        }


        //doMovement(DT, IP);
        
	}

    private void FixedUpdate()
    {
        switch (CurrentState)
        {
            case States.MovementState:
                doMovementState();
                break;
            case States.CombatState:
                doCombatState();
                break;
            case States.MovementStateSprint:
                doMovementStateSprint();
                break;
        }
    }
}
