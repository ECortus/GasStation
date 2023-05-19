using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeTriggerCollision : MonoBehaviour
{
    [SerializeField] private Fridge fridge;
    [SerializeField] private float delayPerTransfer = 0.5f;
    GoodsPickUp pickUp;
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
            if(pickUp.Amount > 0)
            {
                fridge.AddAmount(1);
                pickUp.DecreaseAmount();

                pickUp.MoveGoodToDestination(fridge.transform);
            }
            else
            {
                StopTransfer();
                break;
            }

            yield return wait;
        }
    }

    private string Tag = "Player";

    [HideInInspector] public bool SomeoneInTrigger = false;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == Tag)
        {
            pickUp = col.GetComponentInChildren<GoodsPickUp>();
            StartTransfer();

            SomeoneInTrigger = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == Tag)
        {
            fridge.TransferMoney();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == Tag)
        {
            StopTransfer();
            SomeoneInTrigger = false;
        }
    }
}
