using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsStorage : Storage
{
    public void TransferToPlayer(GoodsPickUp pickUp)
    {
        GetLastModel().ArriveToPickUp(pickUp);
    }
}
