using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveSliderUI : MonoBehaviour
{
    [Header("Info slider: ")]
    [SerializeField] private Slider slider;
    [SerializeField] private float TimeToComplete = 2f;
    private string Tag = "Player";

    bool available = true;

    void Start()
    {
        Setup();
    }

    void Allow()
    {
        available = true;
    }

    void Forbid()
    {
        available = false;
    }
    
    void Setup()
    {
        slider.interactable = false;

        slider.minValue = 0f;
        slider.maxValue = 1f;
        
        slider.value = -99999f;
    }

    void AddValue()
    {
        slider.value += Time.deltaTime / TimeToComplete;

        if(slider.value >= 1f)
        {
            Complete();

            Forbid();
            Setup();
        }
    }

    protected virtual bool ConditionToAllowInter { get; }
    protected virtual void Complete() {}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == Tag)
        {
            Allow();
            Setup();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(!available || !ConditionToAllowInter) return;

        if(col.tag == Tag)
        {
            AddValue();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == Tag)
        {
            Setup();
        }
    }
}
