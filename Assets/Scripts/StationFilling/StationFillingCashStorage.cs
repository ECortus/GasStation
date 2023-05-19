using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationFillingCashStorage : CashStorage
{
    public void AddCash()
    {
        Add();
    }

    public void TransferAllToPlayer()
    {
        ReduceInArriveAll();
    }
}
