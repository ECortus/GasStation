using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarUI : MonoBehaviour
{
    public static StarUI Instance { get; set; }

    [SerializeField] private TextMeshProUGUI StarText;
    [SerializeField] private float counterPlusBySecond = 0.1f;

    private static int Star { get { return Statistics.Star; } set { Statistics.Star = value; } }
    int currentStarCount = 0;
    [SerializeField] private int bound = 3;

    Coroutine coroutine;

    void Awake() => Instance = this;

    void Start()
    {
        /* Star.Load(); */
        ResetStar();
    }

    public void UpdateStar()
    {
        if(coroutine == null && currentStarCount != Star) coroutine = StartCoroutine(Coroutine());
    }

    public void ResetStar()
    {
        currentStarCount = Star;
        IntoText(currentStarCount);
    }

    IEnumerator Coroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.014f);

        while(currentStarCount != Star)
        {
            currentStarCount = (int)Mathf.Lerp(currentStarCount, Star, counterPlusBySecond * Time.deltaTime);
            if (Mathf.Abs(currentStarCount - Star) <= bound) break;

            IntoText(currentStarCount);

            yield return wait;
        }

        ResetStar();
        yield return null;

        /* StopCoroutine(coroutine); */
        coroutine = null;
    }

    void IntoText(int value)
    {
        int Star = value;
        string text = $"{Star}";

        /* string text = "";
        int thousands = Star / 1000;
        int hundreds = Star % 1000;

        if(thousands == 0) text = $"{hundreds}";
        else
        {
            text = $"{thousands}." + $"{hundreds / 10}K";
        } */

        StarText.text = text;
    }
}
