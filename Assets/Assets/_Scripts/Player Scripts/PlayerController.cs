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
    public float SmoothDamp = 10;

    public GameObject Bullet;

    public Camera Cam;

    public Vector3 IP; // Movement Input

    States CurrentState;

    float DT;


    public float setAttackDelay = 1.0f;
    private float attackDelay = 0.0f;

    public bool attacking;
	// Use this for initialization
	void Start () {

        RB = GetComponent<Rigidbody>(); //Gets Rigidbody on object
        // anim passed in through editor
	}
	
    public void KeyInput()
    {
        IP.x = Input.GetAxisRaw("Horizontal");
        IP.z = Input.GetAxisRaw("Vertical");
        attacking = Input.GetMouseButton(0);
    }

    public void doMovement(float DeltaTime, Vector3 MoveInput, Transform DirTrans)
    {
        if (RB != null)
        {
            Vector3 Forward = DirTrans.forward * IP.z;  //Transform forward multiplied by our vertical input
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

    public void doCombat()
    {
        doMovementState();

        attackDelay -= Time.deltaTime;

        if(attacking && attackDelay <= 0.0f)
        {
            GameObject temp = Instantiate(Bullet, transform.position, transform.rotation);
            temp.GetComponent<Rigidbody>().velocity = transform.forward * 20;
            attackDelay = setAttackDelay;
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

    public void doLook() //rotate player towards mouse cursor
    {
        RaycastHit hit;

        Ray ray = Cam.ScreenPointToRay(Input.mousePosition); // Creating ray from mouse point on screen

        if(Physics.Raycast(ray, out hit, 1000000)) // casting out a ray and populating our hit vaue
        {

            Vector3 forward = (transform.position - hit.point).normalized * -1; // getting the direction between our position and or hit.point position
            forward.y = 0; // zero the y to stay upright
            forward.Normalize(); // normalize to calculate direction
            transform.forward = Vector3.MoveTowards(transform.forward, forward, Time.deltaTime * SmoothDamp); //  move our forward towards the direction between the positions

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

        doCombat();

        //if (CurrentState != States.CombatState)
        //{
        //    if (Input.GetKey(KeyCode.LeftShift))
        //    {
        //        CurrentState = States.MovementStateSprint;
        //    }
        //    else
        //    {
        //        CurrentState = States.MovementState;
        //    }
        //}


        //doMovement(DT, IP);
        
	}

    private void FixedUpdate()
    {

        doLook();

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
