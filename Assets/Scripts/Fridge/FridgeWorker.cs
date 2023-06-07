using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FridgeWorker : Worker
{
    Coroutine coroutine;

    [SerializeField] private List<Fridge> fridges = new List<Fridge>();

    [Space]
    [SerializeField] private FridgeTriggerCollision trigger;
    [SerializeField] private GoodsPickUp pickUp;
    [SerializeField] private Transform boxesPoint, restPoint;

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

            foreach(Fridge fridge in fridges)
            {
                if(fridge.Level < 0) continue;

                if(fridge.FillAmount != fridge.MaxFillAmount)
                {
                    value = false;
                    break;
                }
            }

            return value;
        }
    }

    Fridge NotFullFridge
    {
        get
        {
            Fridge notfull = null;

            foreach(Fridge fridge in fridges)
            {
                if(fridge.Level < 0) continue;
                
                if(fridge.FillAmount != fridge.MaxFillAmount)
                {
                    notfull = fridge;
                    break;
                }
            }

            return notfull;
        }
    }

    IEnumerator Work()
    {
        Vector3 fridgePoint = new Vector3();
        Fridge fridge = null;

        while(true)
        {
            while(AllFull)
            {
                SetTarget(restPoint.position);
                yield return new WaitForSeconds(1f);
            }

            fridge = NotFullFridge;

            if(fridge != null)
            {
                fridgePoint = fridge.transform.position - Vector3.forward * 2f;

                SetTarget(boxesPoint.position);
                yield return new WaitForSeconds(1f);
                yield return new WaitUntil(() => Agent.velocity.magnitude < 0.1f);

                SetPickUpAnimation(true);
                /* yield return new WaitForSeconds(3f); */
                 yield return new WaitUntil(() => 
                    pickUp.Amount == pickUp.MaxAmount || pickUp.Amount + fridge.FillAmount >= fridge.MaxFillAmount);

                SetTarget(fridgePoint);
                yield return new WaitForSeconds(1f);
                yield return new WaitUntil(() => Agent.velocity.magnitude < 0.1f);

                yield return new WaitUntil(() => 
                    fridge.FillAmount == fridge.MaxFillAmount || pickUp.Amount == 0);

                /* yield return new WaitForSeconds(5f); */
                SetPickUpAnimation(false);
                if(pickUp.Amount > 0) pickUp.RemoveAndDisableAllBoxes();
            }
        }
    }
}
