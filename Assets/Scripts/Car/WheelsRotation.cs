using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<Transform> wheels;
    [SerializeField] private float scale = 1f;

    float speed;
    int sign
    {
        get
        {
            int i = 0;
            if(Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.z)) i = rb.velocity.x > 0f ? 1 : -1;
            else i = rb.velocity.z > 0f ? 1 : -1;
            return i;
        }
    }

    void Update()
    {
        if(rb.velocity.magnitude < 0.1f) return;

        speed = rb.velocity.magnitude * scale * sign;

        foreach(Transform wheel in wheels)
        {
            wheel.Rotate(new Vector3(0f, speed, 0f), Space.Self);
        }
    }
}
