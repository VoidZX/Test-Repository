using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // include AI
public class AIController : MonoBehaviour {

    public enum AIStates
    {
        ChaseState,
        AttackState,
        DeathState
    }

    public AIStates CurrentState;

    public GameObject TargetPoint; // point to move towards
    private NavMeshAgent agent;

    public float AttackRange = 10.0f;
    public float AttackDelay;
    public bool IsAttacking;
    public float SetAttackDelay;

    public Rigidbody RB;

    public Animator Anim;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(TargetPoint.transform.position);
	}
	
    public bool InAttackRange()
    {
        float DistanceToTarget = agent.remainingDistance;

        if (DistanceToTarget <= AttackRange) // is target within attack range?
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void doChase(GameObject target)
    {
        if (target == null) { Debug.Log("Null Reference: AI target not found"); } // Null Reference Checker

        if (agent.isStopped) { agent.isStopped = false; } // Set Stopped to false

        float DistanceToTarget = agent.remainingDistance;

        if (InAttackRange()) // is target within attack range?
        {
            CurrentState = AIStates.AttackState;
            return;
        }


        agent.SetDestination(target.transform.position);
    }

    public void doAttack(float DeltaTime)
    {
        Debug.Log("attacking");

        if (!agent.isStopped) { agent.isStopped = true; } // Set Stopped to true

        if (!InAttackRange() && IsAttacking)
        {
            CurrentState = AIStates.ChaseState;
            return;
        }

        SetAttackDelay -= DeltaTime;

        if (SetAttackDelay <= 0)
        {
            Anim.SetTrigger("Attack");
            IsAttacking = true;
            SetAttackDelay = AttackDelay;
        }

        agent.SetDestination(TargetPoint.transform.position);
    }

    public void doDeath(bool isDead)
    {

    }

    public void updateAnim(Animator AnimationController)
    {
        if (AnimationController != null)
        {
            var localVel = transform.InverseTransformDirection(agent.velocity);
            AnimationController.SetFloat("ForwardSpeed", localVel.z);
        }
    }

    // Update is called once per frame
    void Update () {

        switch(CurrentState) // Current State = ...
        {
            case AIStates.ChaseState:
                doChase(TargetPoint);
                break;
            case AIStates.AttackState:
                doAttack(Time.deltaTime);
                break;
            case AIStates.DeathState:
                doDeath(false); //TODO: Create death variable to check
                break;
        }
    updateAnim(Anim);
    }
}
