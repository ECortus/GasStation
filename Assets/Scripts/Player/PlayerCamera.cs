using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance { get; set; }

    [SerializeField] private Transform target;
    public void SetTarget(Transform trg)
    {
        target = trg;
    }
    public void ResetTarget()
    {
        target = Player.Instance.Transform;
    }

    public float speedMove, speedRotate = 2f;

    private Vector3 defaultPosition, defaultRotation;

    private Vector3 position => target.position + defaultPosition;
    private Vector3 rotation => defaultRotation/*  + target.eulerAngles */;

    [Space]
    [SerializeField] private PinOnFollowObject pin;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pin.enabled = false;
        ResetTarget();

        defaultPosition = transform.position - target.position;
        defaultRotation = transform.eulerAngles;

        ResetCamera();
    }

    public void ResetCamera()
    {
        transform.position = position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, position, speedMove * Time.deltaTime
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation, Quaternion.Euler(rotation), speedRotate * Time.deltaTime
        );
    }
}
