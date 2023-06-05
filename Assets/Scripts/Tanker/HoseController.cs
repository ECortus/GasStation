using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseController : MonoBehaviour
{
    public string Target;

    [SerializeField] private Transform toHold;
    private Transform defaultParent;
    [SerializeField] private Vector3 defPos;

    void Start()
    {
        defaultParent = toHold.parent;
    }

    public void ChangeParent(Transform parent = null)
    {
        if(parent == null)
        {
            toHold.parent = defaultParent;
        }
        else
        {
            toHold.parent = parent;
        }
    }

    Vector3 pos;

    void Update()
    {
        if(toHold.parent != defaultParent) pos = Vector3.zero;
        else pos = defPos;

        toHold.localEulerAngles = Vector3.Lerp(toHold.localEulerAngles, Vector3.zero, 5f * Time.deltaTime);
        toHold.localPosition = Vector3.Lerp(toHold.localPosition, pos, 5f * Time.deltaTime);
    }
}
