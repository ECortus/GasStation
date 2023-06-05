using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    private static readonly int _Speed = Animator.StringToHash("Speed");
    private static readonly int _PickUp = Animator.StringToHash("PickUp");

    [SerializeField] private Animator Animator;
    public NavMeshAgent Agent;
    [SerializeField] private Rigidbody rb;

    private Vector3 target;
    public void SetTarget(Vector3 trg = new Vector3())
    {
        target = trg;
    }

    public bool Active = true;
    bool isPickUpSomething = false;
    public void SetPickUpAnimation(bool value) => isPickUpSomething = value;

    private void Update()
    {
        if(Active && target != new Vector3())
        {
            UpdateAnimator();
            Move();
            Rotate();
        }
        else
        {
            Agent.velocity = Vector3.zero;
            UpdateAnimator();
        }
    }

    private void UpdateAnimator()
    {
        Animator.SetFloat(_Speed, Agent.velocity.magnitude);
        Animator.SetBool(_PickUp, isPickUpSomething);
    }

    private void Move()
    {
        if (Agent.isActiveAndEnabled)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Agent.SetDestination(target);
        }
    }

    private void Rotate()
    {
        if (Agent.isActiveAndEnabled)
        {
            var targetRotation = Quaternion.LookRotation(target - transform.position);
            targetRotation.x = 0f;
            targetRotation.z = 0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * Agent.angularSpeed);
        }
    }
}
