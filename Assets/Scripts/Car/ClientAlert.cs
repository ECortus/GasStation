using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientAlert : MonoBehaviour
{
    public void On()
    {
        anim.Play("ShowAlert");
    }

    public void Off()
    {
        anim.Play("HideAlert");
    }

    [SerializeField] private Animation anim;
}
