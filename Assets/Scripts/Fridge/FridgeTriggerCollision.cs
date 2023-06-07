using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeTriggerCollision : MonoBehaviour
{
    [SerializeField] private Fridge fridge;
    [SerializeField] private float delayPerTransfer = 0.5f;
    public GoodsPickUp pickUp { get; set; }
    Coroutine coroutine;

    public void StartTransfer()
    {
        if(coroutine == null) coroutine = StartCoroutine(Transfer());
    }

    public void StopTransfer()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Transfer()
    {
        WaitForSeconds wait = new WaitForSeconds(delayPerTransfer);

        while(true)
        {
            if(pickUp.Amount > 0 && fridge.MaxFillAmount != fridge.FillAmount)
            {
                fridge.AddAmount(1);
                pickUp.DecreaseAmount();

                pickUp.MoveGoodToDestination(fridge.transform);
            }
            else
            {
                /* StopTransfer();
                break; */
                yield return new WaitUntil(() => pickUp);
            }

            yield return wait;
        }
    }

    [SerializeField] private GameObject worker;
    private List<string> Tags = new List<string>{"Player", "Worker"};

    [HideInInspector] public bool SomeoneInTrigger = false;

    void OnTriggerEnter(Collider col)
    {
        if(Tags.Contains(col.tag))
        {
            if(col.tag == "Player" && worker.activeSelf) return;
            
            pickUp = col.GetComponentInChildren<GoodsPickUp>();
            StartTransfer();

            SomeoneInTrigger = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == Tags[0])
        {
            fridge.TransferMoney();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(Tags.Contains(col.tag))
        {
            if(col.tag == "Player" && worker.activeSelf) return;

            StopTransfer();
            SomeoneInTrigger = false;

            pickUp = null;
        }
    }
}
