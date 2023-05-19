using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    [SerializeField] private Car car;

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player" || col.tag == "Car")
        {
            car.SetMotor(0);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if((col.tag == "Player" || col.tag == "Car") && car.target != null)
        {
            car.SetMotor(1);
        }
    }
}
