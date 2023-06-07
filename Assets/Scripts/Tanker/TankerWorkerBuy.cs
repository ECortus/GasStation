using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerWorkerBuy : WorkerBuy
{
    [SerializeField] private Collider col;
    [SerializeField] private GameObject canvas;
    [SerializeField] private List<StationFilling> stations;

    bool condition
    {
        get
        {
            bool value = true;
            bool hasOne = false;

            foreach(StationFilling station in stations)
            {
                if(station.Level < 0)
                {
                    value = false;
                }
                else
                {
                    hasOne = true;
                    value = true;
                }
            }

            return value || hasOne;
        }
    }

    void Update()
    {
        if(!canvas.activeSelf && condition)
        {
            canvas.SetActive(true);
            col.enabled = true;
        }
        else if(canvas.activeSelf && !condition)
        {
            canvas.SetActive(false);
            col.enabled = false;
        }
    }
}
