using Lib.Joystick_Pack;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }
    public Transform Transform { get { return transform; } }

    private static readonly int _Speed = Animator.StringToHash("Speed");
    private static readonly int _PickUp = Animator.StringToHash("PickUp");

    [SerializeField] private Animator Animator;
    private FloatingJoystick joyStick => GameManager.Instance.Joystick;
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private Rigidbody rb;

    private Vector3 direction;

    public bool Active = true;
    bool isPickUpSomething = false;
    public void SetPickUpAnimation(bool value) => isPickUpSomething = value;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if(Active)
        {
            UpdateDirection();
            UpdateAnimator();
            Move();
            Rotate();
        }
        else
        {
            Agent.velocity = Vector3.zero;
            direction = Vector3.zero;
            UpdateAnimator();
        }
    }

#region Movement
    private void UpdateDirection()
    {
        direction = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical).normalized;
        direction = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * direction;
    }

    private void UpdateAnimator()
    {
        Animator.SetFloat(_Speed, direction.magnitude);
        Animator.SetBool(_PickUp, isPickUpSomething);
    }

    private void Move()
    {
        if (Agent.isActiveAndEnabled && direction != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Agent.Move(direction * (Agent.speed * Time.deltaTime));
        }
    }

    private void Rotate()
    {
        if (Agent.isActiveAndEnabled && direction != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation((transform.position + direction) - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * Agent.angularSpeed);
        }
    }

#endregion

    public void TeleportToPoint(Vector3 point)
    {
        int mask = LayerMask.NameToLayer("NavMesh");
        NavMesh.SamplePosition(point, out var hit, 5f, mask);
        if (hit.hit)
        {
            Agent.enabled = false;
            transform.position = point;
            
            PlayerCamera.Instance.ResetCamera();
            Agent.enabled = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            default:
                break;
        }
    }

    void OnTriggerExit(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            default:
                break;
        }
    }
}

