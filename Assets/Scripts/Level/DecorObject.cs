using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorObject : MonoBehaviour
{
    public bool Buyed
    {
        get
        {
            return PlayerPrefs.GetInt(gameObject.name + "Decor", -1) > 0 ? true : false;
        }
        set
        {
            PlayerPrefs.SetInt(gameObject.name + "Decor", value ? 1 : -1);
            PlayerPrefs.Save();
        }
    }

    [SerializeField] private Animation buy, sell;
    public int Cost = 150;

    void Start()
    {
        if(Buyed)
        {
            Buy();
        }
        else
        {
            Hide();
        }
    }

    public void Buy()
    {
        buy.Play("ShowConstruction");
        sell.Play("HideConstruction");

        Buyed = true;

        this.enabled = false;
    }

    public void Hide()
    {
        sell.Play("ShowConstruction");
        buy.Play("HideConstruction");
    }
}
