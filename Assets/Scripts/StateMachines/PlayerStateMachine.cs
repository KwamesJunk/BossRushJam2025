using UnityEngine;

[RequireComponent (typeof(InputReader))]
public class PlayerStateMachine : StateMachine
{
    private InputReader _reader;
    [SerializeField] public float MaxRunSpeed;
    [SerializeField] public float BaseGravity;
    [SerializeField] public AttackCombo BasicCombo;
    [SerializeField] public float jumpSpeed;
    [SerializeField] public Transform weaponMeshSlot;
    [SerializeField] private Transform _weaponSlot;

    public bool IsMoving => _reader.Movement != Vector2.zero;
    public Vector3 HorizontalDirection;
    public float CurrentGravity;
    public float VerticalSpeed;
    public Vector3 currentVelocity;
    public Transform WeaponSlot => _weaponSlot;
    public InputReader Reader => _reader;

    void Start()
    {
        InitializeComponents();
        _reader = GetComponent<InputReader>();

        Transition(new LocomotionState(this));
        CurrentGravity = BaseGravity;
    }

    // Update is called once per frame
    void Update()
    {
        // This should probably go somewhere else
        VerticalSpeed += CurrentGravity * Time.deltaTime;
        if (IsGrounded() && VerticalSpeed < 0) 
        {
            VerticalSpeed = 0;
        }

        // work out velocity stuff here

        _currentState?.Tick(Time.deltaTime);

        if (transform.position.y < -20)
        {
            transform.position = Vector3.zero;
        }
    }

    
    public void MoveCharacter(Vector3 movement)
    {
        Vector3 verticalVelocity = transform.up * VerticalSpeed;

        charController.Move((movement + verticalVelocity) * Time.deltaTime);
    }

    public void MoveCharacter()
    {
        MoveCharacter(Vector3.zero);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position+charController.center+(Vector3.down*(charController.height*0.5f)), Vector3.down, maxDistance: 0.1f);
    }

    // private void OnTakeDamage(int damage)
    // {
    //     Debug.Log($"Ow! ({damage})");
    //     Transition(new MediumHitState(this));
    // }

    public void AddJumpSpeed()
    {
        //Debug.Log($"Before VS: {VerticalSpeed} JS:{jumpSpeed}");
        VerticalSpeed += jumpSpeed;
        Debug.Log($"After VS: {VerticalSpeed} JS:{jumpSpeed}");
    }
}
