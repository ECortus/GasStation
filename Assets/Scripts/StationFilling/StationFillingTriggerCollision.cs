using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationFillingTriggerCollision : MonoBehaviour
{
    [SerializeField] private StationFilling station;
    private string Tag = "Player";

    /* void OnTriggerEnter(Collider col)
    {
        if(col.tag == Tag)
        {

        }
    } */

    void OnTriggerStay(Collider col)
    {
        if(col.tag == Tag)
        {
            station.Transfer();
        }
    }

    /* void OnTriggerExit(Collider col)
    {
        if(col.tag == Tag)
        {

        }
    } */
}
