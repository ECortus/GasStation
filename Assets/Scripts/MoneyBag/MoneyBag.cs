using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public int Amount = 1000;

    public void PlusToPlayer()
    {
        RewardFunctions.Instance.PlusMoneyFromBag(Amount);
    }
}
