using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsTriggerCollision : MonoBehaviour
{
    [SerializeField] private GoodsStorage storage;
    [SerializeField] private GoodsAmountCanvas info;
    [SerializeField] private List<Fridge> fridges;

    [Space]
    [SerializeField] private float delayPerTransfer = 0.5f;
    GoodsPickUp pickUp;
    Coroutine coroutine;

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

    int RequireBoxes
    {
        get
        {
            int value = 0;

            foreach(Fridge fridge in fridges)
            {
                if(fridge.Level < 0) continue;

                if(fridge.FillAmount != fridge.MaxFillAmount)
                {
                    value += fridge.MaxFillAmount - fridge.FillAmount;
                }
            }

            return value;
        }
    }

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
            if(AllFull) break;

            if(pickUp.MaxAmount > pickUp.Amount && pickUp.Amount < RequireBoxes)
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

    RopeHolder rope;

    void OnTriggerEnter(Collider col)
    {
        if(Tags.Contains(col.tag))
        {
            if(col.tag == "Player" && worker.activeSelf) return;

            if(col.tag == "Player")
            {
                rope = GetComponent<RopeHolder>();
                if(rope.Hold)
                {
                    rope.ResetParent();
                }
            }

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

            pickUp = null;
        }
    }
}
