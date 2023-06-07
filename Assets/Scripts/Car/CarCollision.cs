using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    [SerializeField] private Car car;

    private List<string> Tags = new List<string>{"Player", "Car", "Worker"};

    void OnTriggerStay(Collider col)
    {
        if(Tags.Contains(col.tag) && car.target != null)
        {
            car.SetMotor(0);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(Tags.Contains(col.tag) && car.target != null)
        {
            car.SetMotor(1);
        }
    }
}
