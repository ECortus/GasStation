using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreeTankerFillSliderUI : InteractiveSliderUI
{
    [Space]
    [SerializeField] private Tanker tanker;
    [SerializeField] private GasTrucksController trucksController;
    
    protected override void Complete()
    {
        /* tanker.Fill(); */
        RewardFunctions.Instance.CallToFillTankerByAD(ref time, adDelay, trucksController);
    }

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return /* tanker.FillAmount < tanker.MaxFillAmount && */ !trucksController.Called && time <= 0f;
        }
    }

    [Space]
    [SerializeField] private float adDelay = 60f;
    [SerializeField] private TextMeshProUGUI fillText;
    float time = 0f;

    void Update()
    {
        if(time > 0f)
        {
            time -= Time.deltaTime;
            Convert(time);
        }
        else
        {
            fillText.text = "FREE\r\nFILL";
        }
    }
    
    void Convert(float seconds)
    {
        int whole = (int)(seconds) / 60;
        int left = (int)(seconds) % 60;

        string text1 = whole.ToString();
        text1 = text1.Length < 2 ? "0" + text1 : text1;

        string text2 = left.ToString();
        text2 = text2.Length < 2 ? "0" + text2 : text2;
        
        fillText.text = text1 + ":" + text2;
    }
}
