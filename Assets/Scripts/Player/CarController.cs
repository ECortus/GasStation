using System;
using UnityEngine;
using DavidJalbert;

public class CarController : MonoBehaviour
{
	public static CarController Instance;
	public Transform Transform { get { return transform; } }
	public Player player => Player.Instance;
	public TinyCarController carController;
	
	public Rigidbody rb;
	public Transform hoodTransform;
	
	[Space]
	private Transform handle;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private float moveSpeed;

	public Transform playerOutTransform;

	public static bool Buyed;

	Vector2 move;

	private void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		handle = GameManager.Instance.Joystick.transform.GetChild(0).GetChild(0);
	}

	private void Update()
	{
		Drive();
	}

#region Movement
	void Drive()
	{
		if(Input.GetMouseButton(0) && handle.localPosition != Vector3.zero)
		{
			Move();
			Rotate();
		}
		else
		{
			carController.setMotor(0);
		}
	}

	void Move()
	{
		carController.maxSpeedForward = moveSpeed;

		move = new Vector2(handle.localPosition.x, handle.localPosition.y);
		carController.setMotor(2);
	}

	void Rotate()
	{
		Vector3 tv = new Vector3(move.x, 0, move.y);
		var rotation = Quaternion.LookRotation(tv);
		rb.MoveRotation(Quaternion.Slerp(transform.localRotation, rotation, rotateSpeed));
	}
#endregion

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(playerOutTransform.position, 0.5f);
	}
}