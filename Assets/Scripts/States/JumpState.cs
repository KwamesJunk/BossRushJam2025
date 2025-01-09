using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    bool goingUp;
    float timeKeeper;
    public JumpState(StateMachine stateMachine) : base(stateMachine)
    {
        _name = "Jump";
    }

    public override void Enter()
    {
        _stateMachine.AddJumpSpeed();
        _animator.CrossFadeInFixedTime("Flip", 0.1f);
        goingUp = true;
        timeKeeper = 0;
        //_animator.enabled = false;
    }

    public override void Tick(float deltaTime)
    {
        timeKeeper += Time.deltaTime;

        _stateMachine.MoveCharacter();
        //_stateMachine.Root.transform.Rotate(new Vector3(1080 * deltaTime, 0, 0));

        if (timeKeeper > 0.25f && _stateMachine.IsGrounded()) {
            _stateMachine.Transition(new LocomotionState(_stateMachine));
        }
    }

    public override void Exit()
    {
        //Vector3 eulerAngles = _stateMachine.Root.transform.eulerAngles;
        //_stateMachine.Root.transform.rotation = Quaternion.Euler(new Vector3(0, eulerAngles.y, eulerAngles.z));
        _animator.enabled = true;
    }
}
