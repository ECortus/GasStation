using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsTriggerCollision : MonoBehaviour
{
    [SerializeField] private GoodsStorage storage;
    [SerializeField] private GoodsAmountCanvas info;

    [Space]
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
            if(pickUp.MaxAmount > pickUp.Amount)
            {
                storage.TransferToPlayer(pickUp);
                pickUp.IncreaseAmount();

                info.Refresh(pickUp);
            }
            else
            {
                yield return new WaitUntil(() => pickUp.MaxAmount > pickUp.Amount);
            }

            yield return wait;
        }
    }

    private string Tag = "Player";

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == Tag)
        {
            pickUp = col.GetComponentInChildren<GoodsPickUp>();
            StartTransfer();

            info.On(pickUp);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == Tag)
        {
            StopTransfer();
            info.Off();
        }
    }
}
