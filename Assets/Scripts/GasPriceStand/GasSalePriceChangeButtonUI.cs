using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GasSalePriceChangeButtonUI : MonoBehaviour, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Button button;
    [SerializeField] private UnityEvent buttonEvent;
    Coroutine coroutine;

    public void StartSim()
    {
        if(coroutine == null) coroutine = StartCoroutine(Work());
    }

    public void StopSim()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!button.interactable) return;
        StartSim();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopSim();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopSim();
    }

    IEnumerator Work()
    {
        int t = 0;
        int iter = 10;

        while(true)
        {
            buttonEvent.Invoke();
            if(t < iter) t++;

            yield return new WaitForSeconds((iter / 20f + 0.05f) - t / (iter * 2f));
        }
    }
}
