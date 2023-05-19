using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressorTriggerCollision : MonoBehaviour
{
    [SerializeField] private Compressor compressor;
    private string Tag = "Player";

    [HideInInspector] public bool SomeoneInTrigger = false;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == Tag)
        {
            SomeoneInTrigger = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == Tag)
        {
            compressor.Transfer();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == Tag)
        {
            SomeoneInTrigger = false;
        }
    }
}
