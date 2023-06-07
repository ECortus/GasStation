using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<Transform> wheels;
    [SerializeField] private float scale = 1f;

    float speed;

    void Update()
    {
        speed = rb.velocity.magnitude * scale;

        foreach(Transform wheel in wheels)
        {
            wheel.Rotate(new Vector3(0f, 0f, speed), Space.Self);
        }
    }
}
