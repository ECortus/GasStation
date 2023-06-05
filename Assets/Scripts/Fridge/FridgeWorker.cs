using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FridgeWorker : Worker
{
    Coroutine coroutine;

    [Space]
    [SerializeField] private FridgeTriggerCollision trigger;
    [SerializeField] private GoodsPickUp pickUp;
    [SerializeField] private Transform boxesPoint, fridgePoint;

    void OnEnable()
    {
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

    IEnumerator Work()
    {
        while(true)
        {
            SetTarget(boxesPoint.position);
            yield return new WaitForSeconds(2f);
            yield return new WaitUntil(() => Agent.velocity.magnitude < 0.05f);

            SetPickUpAnimation(true);
            yield return new WaitForSeconds(3f);

            SetTarget(fridgePoint.position);
            yield return new WaitForSeconds(2f);
            yield return new WaitUntil(() => Agent.velocity.magnitude < 0.05f);

            yield return new WaitUntil(() => pickUp.Amount == 0 || trigger.pickUp != pickUp);
            
            if(pickUp.Amount == 0) SetPickUpAnimation(false);
        }
    }
}
