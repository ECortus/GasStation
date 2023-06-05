using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Star
{
    private static string key = DataManager.StarKey;
    private static int star { get { return Statistics.Star; } set { Statistics.Star = value; } }

    public static void Plus(int count) 
    {
        star += count;

        StarUI.Instance.UpdateStar();
    }
    
    public static void Minus(int count)
    { 
        star -= count;
        if(star < 0) star = 0;

        StarUI.Instance.UpdateStar();
    }
}
