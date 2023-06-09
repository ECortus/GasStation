using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    private bool _GameActive = false;
    public void SetActive(bool value) => _GameActive = value;
    public bool GetActive() => _GameActive;

    void Awake() => Instance = this;

    [Header("Joystick:")]
    public FloatingJoystick Joystick;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void SetFollowTarget(Transform tf)
    {
        
    }
}
