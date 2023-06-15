using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeCashStorage : CashStorage
{
    public void AddCash()
    {
        Add();
    }

    public void TransferAllToPlayer()
    {
        ReduceInArriveAll();
        AdTimer.Instance.TryOn();
    }
}
