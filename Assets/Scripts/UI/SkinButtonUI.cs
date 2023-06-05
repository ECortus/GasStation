using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinButtonUI : MonoBehaviour
{
    [SerializeField] private SkinsShop shop;

    [Space]
    public bool Buyed;
    public bool Equiped;
    public int Cost;
    [HideInInspector] public GameObject model;

    [Space]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void OnButtonClick()
    {
        if(Buyed)
        {
            shop.SetChosed(this);
        }
        else
        {
            if(Statistics.Money >= Cost)
            {
                Buyed = true;
                Money.Minus(Cost);
            }
        }

        shop.RefreshButtons();
    }

    public void Refresh()
    {
        if(!Buyed)
        {
            costText.text = $"{Cost}";
            buttonText.text = "Buy";
        }
        else
        {
            costText.text = $"---";
            
            Equiped = shop.GetChosed() == this;
            if(Equiped)
            {
                model.SetActive(true);
                buttonText.text = "Unequip";
            }
            else
            {
                model.SetActive(false);
                buttonText.text = "Equip";
            }
        }
    }
}
