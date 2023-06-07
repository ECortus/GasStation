using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerWorker : Worker
{
    Coroutine coroutine;

    [SerializeField] private List<StationFilling> stations = new List<StationFilling>();

    [Space]
    [SerializeField] private Tanker tanker;
    [SerializeField] private RopeHolder ropeHolder;
    [SerializeField] private Transform tankerRopePoint, restPoint;

    void OnEnable()
    {
        transform.position = restPoint.position;
        StartWork();
    }

    void OnDisable()
    {
        StopWork();
    }

    public void StartWork()
    {
        if(coroutine == null) coroutine = StartCoroutine(Work());
    }

    public void StopWork()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    bool AllFull
    {
        get
        {
            bool value = true;

            foreach(StationFilling station in stations)
            {
                if(station.Level < 0) continue;

                if(station.FillAmount != station.MaxFillAmount)
                {
                    value = false;
                    break;
                }
            }

            return value;
        }
    }

    StationFilling NotFullStation
    {
        get
        {
            StationFilling notfull = null;

            foreach(StationFilling station in stations)
            {
                if(station.Level < 0) continue;
                
                if(station.FillAmount != station.MaxFillAmount)
                {
                    notfull = station;
                    break;
                }
            }

            return notfull;
        }
    }

    IEnumerator Work()
    {
        Vector3 stationPoint = new Vector3();
        StationFilling station = null;

        while(true)
        {
            while(AllFull || tanker.FillAmount == 0)
            {
                SetTarget(restPoint.position);
                yield return new WaitForSeconds(1f);
            }

            station = NotFullStation;

            if(station != null)
            {
                stationPoint = station.barrelRopeParent.position - Vector3.right / 3f;

                SetTarget(tankerRopePoint.position);
                yield return new WaitForSeconds(1f);
                yield return new WaitUntil(() => Agent.velocity.magnitude < 0.1f || ropeHolder.Hold);

                SetPickUpAnimation(true);

                SetTarget(stationPoint);
                yield return new WaitForSeconds(1f);
                yield return new WaitUntil(() => Agent.velocity.magnitude < 0.1f || !ropeHolder.Hold);

                SetPickUpAnimation(false);

                yield return new WaitForSeconds(2f);
            }
        }
    }
}
