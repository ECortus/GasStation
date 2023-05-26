using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorObject : MonoBehaviour
{
    bool buyed = false;

    public bool Buyed
    {
        get
        {
            return buyed;
        }
        set
        {
            buyed = value;
        }
    }

    [SerializeField] private Animation buy, sell;

    void Start()
    {
        if(Buyed)
        {
            Buy();
        }
    }

    public void Buy()
    {
        buy.Play("ShowConstruction");
        sell.Play("HideConstruction");

        this.enabled = false;
    }
}
