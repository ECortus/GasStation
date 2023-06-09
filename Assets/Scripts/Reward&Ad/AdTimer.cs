using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdTimer : MonoBehaviour
{
    public static AdTimer Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private float time = 0f;

    [Space]
    [SerializeField] private float minTime = 60f;
    [SerializeField] private float maxTime = 120f;
    [SerializeField] private GameObject adCanvas;

    void Start()
    {
        Reset();
    }

    public void TryOn()
    {
        if(time <= 0f)
        {
            On();
        }
    }

    void On()
    {
        GameManager.Instance.Joystick.gameObject.SetActive(false);
        adCanvas.SetActive(true);

        /* Reset(); */
    }

    void Off()
    {
        adCanvas.SetActive(false);
        GameManager.Instance.Joystick.gameObject.SetActive(true);
    }

    public void Reset()
    {
        Off();
        time = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        if(time > 0f)
        {
            time -= Time.deltaTime;
        }
    }
}
