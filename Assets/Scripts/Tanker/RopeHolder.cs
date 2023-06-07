using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RopeHolder : MonoBehaviour
{
    [HideInInspector] public string Target = "";

    public bool _Hold = false;
    public bool Hold
    {
        get
        {
            return _Hold;
        }
        set
        {
            if(value)
            {
                pickUpAnim.Invoke();
            }
            else
            {
                unPickUpAnim.Invoke();
            }

            _Hold = value;
        }
    }
    [SerializeField] private Transform parent;
    private HoseController hose;

    [SerializeField] private UnityEvent pickUpAnim, unPickUpAnim;

    float time = -999f;
    float delayTime = 1.2f;

    void Update()
    {
        if(time > 0f) time -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if(time > 0f) return;

        if(col.tag == "RopePlace")
        {
            if(hose != null && col.GetComponent<HoseController>() == hose) 
            {
                ResetParent();
                return;
            }
            else if(hose != null)
            {
                ResetParent();
            }

            hose = col.GetComponent<HoseController>();
            if(gameObject.tag == "Player" && hose.Target == "Station" && hose.WorkerActive) return;

            RefreshParent(parent);
        }
    }

    public void RefreshParent(Transform parent = null)
    {
        if(!Hold || parent != null)
        {
            hose.ChangeParent(parent);
            Target = hose.Target;
            Hold = true;

            time = delayTime;
        }
    }

    public void ResetParent()
    {
        hose.ChangeParent();
        Target = "";
        Hold = false;

        hose = null;

        time = delayTime;
    }
}
