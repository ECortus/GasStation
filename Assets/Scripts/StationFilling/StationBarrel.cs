using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StationBarrel : MonoBehaviour
{
    [SerializeField] private StationFilling station;
    [SerializeField] private Transform hoseParent;
    RopeHolder rope;

    private List<string> Tags = new List<string>{"Player", "Worker"};

    void OnTriggerEnter(Collider col)
    {
        if(station.Level < 0) return;

        if(Tags.Contains(col.tag))
        {
            if(col.tag == "Player" && station.WorkerActive) return;

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
        await UniTask.Delay(1500);

        rope.ResetParent();
        rope = null;
    }
}
