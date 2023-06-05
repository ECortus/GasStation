using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsShop : MonoBehaviour
{
    private SkinButtonUI ChosedSkin;
    public void SetChosed(SkinButtonUI skin)
    {
        ChosedSkin = skin;
    }
    public SkinButtonUI GetChosed()
    {
        if(ChosedSkin == null)
        {
            return skinButtons[0];
        }

        return ChosedSkin;
    }
    
    [Space]
    [SerializeField] private GameObject menu;

    [Space]
    [SerializeField] private List<GameObject> modelObjects = new List<GameObject>();
    [SerializeField] private List<SkinButtonUI> skinButtons = new List<SkinButtonUI>();
    public void RefreshButtons()
    {
        int i = 0;
        foreach(SkinButtonUI skin in skinButtons)
        {
            skin.model = modelObjects[i];
            skin.Refresh();

            i++;
        }
    }

    void OnEnable()
    {
        RefreshButtons();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            menu.SetActive(true);
        }
    }
}
