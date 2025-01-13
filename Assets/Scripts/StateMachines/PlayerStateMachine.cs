using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] public InputReader Reader;
    [SerializeField] public float MaxRunSpeed;
    [SerializeField] public float BaseGravity;
    [SerializeField] public AttackCombo BasicCombo;
    [SerializeField] public float jumpSpeed;
    [SerializeField] public Transform weaponMeshSlot;
    [SerializeField] private Transform _weaponSlot;

    public bool IsMoving => Reader.Movement != Vector2.zero;
    public Vector3 HorizontalDirection;
    public float CurrentGravity;
    public float VerticalSpeed;
    public Vector3 currentVelocity;
    public Transform WeaponSlot => _weaponSlot;


    void Start()
    {
        Transition(new LocomotionState(this));
        CurrentGravity = BaseGravity;
        //rootOffset = Root.transform.localPosition;
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

    public void Transition (State newState)
    {
        Debug.Log($"Transition from {_currentState} to {newState}");
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();

        _currentStateName = _currentState.Name;
    }

    public void MoveCharacter(Vector3 movement)
    {
        Vector3 verticalVelocity = transform.up * VerticalSpeed;

        CharController.Move((movement + verticalVelocity) * Time.deltaTime);
    }

    public void MoveCharacter()
    {
        MoveCharacter(Vector3.zero);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position+CharController.center+(Vector3.down*(CharController.height*0.5f)), Vector3.down, maxDistance: 0.1f);
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
