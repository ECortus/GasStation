using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jars : MonoBehaviour
{
    [SerializeField] private Fridge fridge;
    [SerializeField] private List<Animation> jars = new List<Animation>();

    void Start()
    {
        for(int i = 0; i < jars.Count; i++)
        {
            jars[i].gameObject.SetActive(false);
        }
    }

    public void Refresh()
    {
        int amount = fridge.FillAmount * (jars.Count / fridge.MaxFillAmount);
        
        for(int i = 0; i < jars.Count; i++)
        {
            if(i >= amount)
            {
                if(jars[i].gameObject.activeSelf)
                {
                    jars[i].gameObject.SetActive(false);
                }
            }
            else
            {
                if(!jars[i].gameObject.activeSelf)
                {
                    jars[i].gameObject.SetActive(true);
                    jars[i].Play("PlaceJar");
                }
            }
        }
    }
}
