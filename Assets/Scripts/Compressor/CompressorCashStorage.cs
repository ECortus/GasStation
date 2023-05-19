using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressorCashStorage : CashStorage
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
