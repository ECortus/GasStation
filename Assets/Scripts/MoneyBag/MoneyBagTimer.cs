using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyBagTimer : MonoBehaviour
{
    [SerializeField] private float TimerSeconds = 180f;
    [SerializeField] private Collider sliderUI;
    [SerializeField] private TextMeshProUGUI timerText;
    float time = 0f;

    void Start()
    {
        Off();
    }

    public void On()
    {   
        timerText.gameObject.SetActive(true);

        sliderUI.enabled = false;
        time = TimerSeconds + 1f;
        Convert(time);

        this.enabled = true;
    }

    public void Off()
    {
        sliderUI.enabled = true;

        timerText.gameObject.SetActive(false);
        this.enabled = false;
    }

    void Update()
    {
        time -= Time.deltaTime;
        Convert(time);

        if(time <= 0.5f)
        {
            Off();
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
        
        timerText.text = text1 + ":" + text2;
    }
}
