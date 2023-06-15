using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class TankerBarrel : MonoBehaviour
{
    public bool Connected = false;

    [SerializeField] private Tanker tanker;
    [SerializeField] private Transform hoseParent;
    RopeHolder rope;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            rope = col.GetComponent<RopeHolder>();
            if(rope != null && rope.Target == "Tanker" && !Connected)
            {
                if(rope.Hold) Fill();
            }
        }
    }

    async void Fill()
    {
        rope.RefreshParent(hoseParent);
        rope.Hold = false;

        Connected = true;

        await UniTask.Delay(3500);

        Connected = false;

        rope.ResetParent();
        rope = null;
    }
}
