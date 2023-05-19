using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Car info: ")]
    [SerializeField] private Rigidbody rb;

    [Space]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speedForward;
    [SerializeField] private AnimationCurve accelerationCurve;

    public bool Active => gameObject.activeSelf;

    int motor = 0;
    public void SetMotor(int mtr)
    {
        motor = mtr;
    }

    [HideInInspector] public Transform target;
    private Vector3 point;
    public void SetTarget(Transform trg)
    {
        target = trg;
        point = trg.position;
    }
    public void ResetTarget()
    {
        target = null;
        point = transform.position;
    }

    float time;

    void FixedUpdate()
    {
        if(motor > 0 && target != null)
        {
            Rotate();
            rb.velocity = transform.forward * speedForward * GetCurveValue();
        }
        else
        {
            time = 0f;
            rb.velocity = Vector3.zero;
        }
    }

    void Rotate()
	{
		Vector3 tv = (point - transform.position).normalized;
        tv.y = 0f;
		var rotation = Quaternion.LookRotation(tv);
        /* rotation.y = 0f; */
		rb.MoveRotation(Quaternion.Slerp(transform.localRotation, rotation, rotateSpeed * Time.fixedDeltaTime));
	}

    float GetCurveValue()
    {
        if(time < 1f) time += Time.fixedDeltaTime;
        return accelerationCurve.Evaluate(time);
    }
}
