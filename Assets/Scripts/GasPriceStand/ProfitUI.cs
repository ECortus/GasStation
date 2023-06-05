using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfitUI : MonoBehaviour
{
    public static ProfitUI Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private GasPriceInfo info;
    [SerializeField] private TextMeshProUGUI plusText, minusText;
    [SerializeField] private GameObject plus, minus;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        float value = System.MathF.Round(info.GasSalePrice - info.GasBuyPrice, 2);

        if(value < 0f)
        {
            plus.SetActive(false);
            minus.SetActive(true);

            minusText.text = $"{value}$";
        }
        else
        {
            plus.SetActive(true);
            minus.SetActive(false);

            plusText.text = $"+{value}$";
        }
    }
}
