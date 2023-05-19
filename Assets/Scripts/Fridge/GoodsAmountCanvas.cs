using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoodsAmountCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void On(GoodsPickUp pickUp)
    {
        gameObject.SetActive(true);
        Refresh(pickUp);
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

    public void Refresh(GoodsPickUp pickUp)
    {
        text.text = $"{pickUp.Amount}/{pickUp.MaxAmount}";
    }
}
