using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RopeHolder : MonoBehaviour
{
    public static RopeHolder Instance { get; set; }
    void Awake() => Instance = this;

    public string Target = "";

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

    void OnTriggerEnter(Collider col)
    {
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
        }
    }

    public void ResetParent()
    {
        hose.ChangeParent();
        Target = "";
        Hold = false;

        hose = null;
    }
}
