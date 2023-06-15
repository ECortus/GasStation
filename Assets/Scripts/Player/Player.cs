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
    [SerializeField] private GoodsPickUp pickUp;

    private Vector3 direction;

    private bool pickUpAnim = false;
    public void SetPickUpAnimation(bool value) => pickUpAnim = value;

    public bool Active = true;

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
        if(joyStick.gameObject.activeSelf)
        {
            direction = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical).normalized;
            direction = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * direction;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void UpdateAnimator()
    {
        Animator.SetFloat(_Speed, direction.magnitude);
        Animator.SetBool(_PickUp, pickUp.Amount > 0 || pickUpAnim);
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

