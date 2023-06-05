using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StationBarrel : MonoBehaviour
{
    [SerializeField] private StationFilling station;
    [SerializeField] private Transform hoseParent;
    RopeHolder rope;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            rope = col.GetComponent<RopeHolder>();
            if(rope != null && rope.Target == "Station")
            {
                if(rope.Hold) Fill();
            }
        }
    }

    async void Fill()
    {
        rope.RefreshParent(hoseParent);
        rope.Hold = false;
        await UniTask.Delay(1500);

        station.Fill();
        await UniTask.Delay(500);

        rope.ResetParent();
        rope = null;
    }
}
