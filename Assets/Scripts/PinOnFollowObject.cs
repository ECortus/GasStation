using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PinOnFollowObject : MonoBehaviour
{
    public Transform pinObject;

    [Space]
    [SerializeField] private bool pinPosition = false;
    public Vector3 Position;
    [SerializeField] private bool pinRotation = false;
    public Vector3 EulerAngles;

    void Update()
    {
        if(pinPosition) transform.position = pinObject.position + Position;
        if(pinRotation) transform.eulerAngles = pinObject.eulerAngles + EulerAngles;
    }
}
