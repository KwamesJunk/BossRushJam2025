using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LocomotionState : State // may refactor this into a locomotion function that's available to all states
{
    private InputReader _reader;
    Vector3 _horizontalDirection;
    float _speed;

    public LocomotionState(StateMachine stateMachine) : base(stateMachine)
    {
        _name = "Locomotion";
    }

    public override void Enter()
    {
        _reader = _stateMachine.Reader;
        _horizontalDirection = _stateMachine.transform.forward;
        _reader.OnAttack += Attack;
        _lifeStats.OnTakeDamage += TakeDamage;
        _animator.CrossFadeInFixedTime("WalkRun", 0.1f);
        _reader.OnJump += Jump;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 inputDirection = Vector3.zero;
        Vector2 movementInput = _reader.Movement;

        if (movementInput != Vector2.zero)
        {
            inputDirection = (movementInput.x * Camera.main.transform.right) + (movementInput.y * Camera.main.transform.forward);
            //_speed += _acceleration * Time.deltaTime;
            inputDirection.y = 0;
            inputDirection.Normalize();
            _stateMachine.transform.rotation = Quaternion.LookRotation(inputDirection);

            _horizontalDirection = inputDirection;
            _speed = _speed = _reader.Movement.magnitude * _stateMachine.MaxRunSpeed;
        }
        else
        {
            _speed = 0;
        }

        Vector3 horizontalVelocity = _horizontalDirection * _speed;
        _stateMachine.CharAnimator.SetBool("Is Grounded", _stateMachine.IsGrounded());
        _stateMachine.CharAnimator.SetFloat("Speed", _speed / _stateMachine.MaxRunSpeed);

        _stateMachine.MoveCharacter(horizontalVelocity);
    }

    public override void Exit()
    {
        _reader.OnAttack -= Attack;
        _lifeStats.OnTakeDamage -= TakeDamage;
        _reader.OnJump -= Jump;
        _stateMachine.TeleportToRoot();
    }

    private void Attack()
    {
        _stateMachine.Transition(new AttackState(_stateMachine));
    }

    private void TakeDamage(int damage)
    {
        _stateMachine.Transition(new MediumHitState(_stateMachine));
    }

    private void Jump()
    {
        _stateMachine.Transition(new JumpState(_stateMachine));
    }
}
