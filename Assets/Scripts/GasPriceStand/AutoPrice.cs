using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AutoPrice : MonoBehaviour
{
    [SerializeField] private float AutoTime;
    [SerializeField] private Button autoPrice, increase, decrease;
    [SerializeField] private UnityEvent equalEvent;
    Coroutine coroutine;
    float time, delta;

    public void On()
    {
        if(coroutine == null) coroutine = StartCoroutine(Auto());
    }

    public void Off()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Auto()
    {
        autoPrice.interactable = false;
        increase.interactable = false;
        decrease.interactable = false;

        delta = Time.deltaTime;
        time = 0f;
        WaitForSeconds wait = new WaitForSeconds(delta);

        while(time < AutoTime)
        {
            equalEvent.Invoke();

            time += delta;
            yield return wait;
        }

        autoPrice.interactable = true;
        increase.interactable = true;
        decrease.interactable = true;
    }
}
