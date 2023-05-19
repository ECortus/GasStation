using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsPickUp : MonoBehaviour
{
    public static GoodsPickUp Instance { get; set; }
    void Awake() => Instance = this;

    public int MaxAmount;
    public int Amount { get; set; }

    [Space]
    public Transform goodsParent;
    public Vector3 defaultGoodsPos, goodsRot;

    public Vector3 goodsPos
    {
        get
        {
            return defaultGoodsPos + Vector3.up / 2f * Amount;
        }
    }

    private List<ModelController> Goods = new List<ModelController>();

    public void IncreaseAmount()
    {
        Amount++;
        Refresh();
    }

    public void DecreaseAmount()
    {
        Amount--;
        Refresh();
    }

    public void AddGood(ModelController mdlC)
    {
        Goods.Add(mdlC);
    }

    public void RemoveGood(ModelController mdlC)
    {
        Goods.Remove(mdlC);
    }

    public void MoveGoodToDestination(Transform trg)
    {
        ModelController mdl = Goods[Goods.Count - 1];
        RemoveGood(mdl);

        mdl.DisableInArrive(trg);
    }

    void Refresh()
    {
        if(Amount > 0)
        {
            Player.Instance.SetPickUpAnimation(true);
        }
        else
        {
            Player.Instance.SetPickUpAnimation(false);
        }
    }
}
