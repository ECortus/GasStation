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
            GameManager.Instance.Joystick.gameObject.SetActive(false);
            adCanvas.SetActive(true);
            /* Reset(); */
        }
    }

    public void Reset()
    {
        adCanvas.SetActive(false);
        GameManager.Instance.Joystick.gameObject.SetActive(true);
        
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
