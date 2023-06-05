using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class RequireStarZoneUI : MonoBehaviour
{
    private int StarAmount;
    [SerializeField] private TextMeshProUGUI amountText;

    [Space]
    [SerializeField] private int Cost;
    [SerializeField] private UnityEvent CompleteEvent;

    private string Tag = "Player";

    Coroutine coroutine;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        StarAmount = Cost;
        RefreshText();
    }

    void RefreshText()
    {
        amountText.text = $"{StarAmount}";
    }

    void StartReduce()
    {
        if(coroutine == null) coroutine = StartCoroutine(ReduceRequiredAmount());
    }

    void StopReduce()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator ReduceRequiredAmount()
    {
        int amount = 1;
        int iter = 0;
        yield return null;

        while(true)
        {
            if(!ConditionToAllowInter) break;

            /* if(Statistics.Star > 10 && StarAmount - 10 > 0) amount = 10;
            else amount = 1; */

            Star.Minus(amount);
            StarAmount -= amount;

            RefreshText();

            if(StarAmount <= 0)
            {
                Complete();
                StopReduce();
                break;
            }

            yield return new WaitForSeconds(0.075f / (1f + iter / 8f));
            iter++;
        }        
    }

    bool ConditionToAllowInter
    {
        get
        {
            return Statistics.Star > 0;
        }
    }
    void Complete() 
    {
        CompleteEvent.Invoke();
    }

    void OnTriggerEnter(Collider col)
    {
        if(!ConditionToAllowInter || !this.enabled) return;

        if(col.tag == Tag)
        {
            StartReduce();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(!this.enabled) return;

        if(col.tag == Tag)
        {
            StopReduce();
        }
    }
}
