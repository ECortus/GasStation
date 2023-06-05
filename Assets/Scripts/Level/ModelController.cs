using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ModelController : MonoBehaviour
{
    public Storage storage { get; set; }

    [SerializeField] private float speedMove, speedRotation, arriveToPlayerBound = 3f;

    private Transform target;
    private Vector3 localPos, localRot;
    private Vector3 Position
    {
        get
        {
            if(target == null) return localPos;
            else return target.position + localPos;
        }
    }

    private Vector3 Rotation
    {
        get
        {
            if(target == null) return localRot;
            else return target.eulerAngles + localRot;
        }
    }

    public void Enable(Storage stg, Transform parent, Vector3 trg, Vector3 rot, Vector3 spawnEA = new Vector3())
    {
        transform.parent = stg.transform;

        storage = stg;
        target = parent;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = spawnEA;

        gameObject.SetActive(true);

        localPos = trg;
        localRot = rot;

        SetMove(1);
    }

    public async void Disable()
    {
        gameObject.SetActive(false);

        await UniTask.Delay(2000);
        storage.AddToPool(this);
    }

    public void ArriveToPickUp(GoodsPickUp pickUp)
    {
        storage.Reduce(this);
        pickUp.AddGood(this);

        transform.parent = pickUp.goodsParent;

        target = null;
        localPos = pickUp.goodsPos;
        localRot = pickUp.goodsRot;

        SetMove(1);

        /* await UniTask.WaitUntil(() => ArriveCondition());

        SetMove(0); */
    }

    public async void DisableInArrive(Transform trg)
    {
        target = trg;

        transform.parent = null;
        localPos = Vector3.zero;
        localRot = Vector3.zero;

        SetMove(1);

        await UniTask.WaitUntil(() => ArriveCondition());

        Disable();
    }

    bool ArriveCondition()
    {
        return Vector3.Distance(transform.position, Position) <= arriveToPlayerBound;
    }

    int move = 0;
    public void SetMove(int mv) => move = mv;

    void Update()
    {  
        Rotate();

        if(move == 0) return;

        if(target != null)
        {
            transform.position = Vector3.Lerp(transform.position, Position, speedMove * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Position, speedMove * Time.deltaTime);
        }

        /* if(transform.position == target && transform.eulerAngles == rotation) this.enabled = false; */
    }

    void Rotate()
    {
        if(target != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Rotation), speedRotation * Time.deltaTime);
        }
        else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(Rotation), speedRotation * Time.deltaTime);
        }
    }
}
