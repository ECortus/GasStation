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
                yield return new WaitUntil(() => storage.Models.Count > 0);

                storage.TransferToPlayer(pickUp);
                pickUp.IncreaseAmount();

                info.Refresh(pickUp);
            }
            else
            {
                StopTransfer();
            }

            yield return wait;
        }
    }

    [SerializeField] private GameObject worker;
    private List<string> Tags = new List<string>{"Player", "Worker"};

    void OnTriggerEnter(Collider col)
    {
        if(Tags.Contains(col.tag))
        {
            if(col.tag == "Player" && worker.activeSelf) return;
            if(col.tag == "Player" && RopeHolder.Instance.Hold) RopeHolder.Instance.ResetParent();

            pickUp = col.GetComponentInChildren<GoodsPickUp>();
            StartTransfer();

            info.On(pickUp);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(Tags.Contains(col.tag))
        {
            if(col.tag == "Player" && worker.activeSelf) return;

            StopTransfer();
            info.Off();
        }
    }
}
