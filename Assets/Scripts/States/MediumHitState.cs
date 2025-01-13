using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumHitState : State
{
    float _timekeeper;

    public MediumHitState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if (_lifeStats.CurrentHp <= 0) {
            _stateMachine.Transition(new DeadState(_stateMachine));
            return;
        }
        _animator.CrossFadeInFixedTime("GotHit", 0.1f);
        _timekeeper = 0;
    }

    public override void Tick(float deltaTime)
    {
        _timekeeper += deltaTime;

        AnimatorStateInfo currentAnimatorState = _animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextAnimatorState = _animator.GetNextAnimatorStateInfo(0);
        AnimatorClipInfo[] animatorClipInfo = _animator.GetCurrentAnimatorClipInfo(0);

        if (currentAnimatorState.IsTag("TakeDamage"))
        {
            //Debug.Log("***CurrentAnimation" + currentAnimatorState.normalizedTime);
            if (currentAnimatorState.normalizedTime >= 1)
            {
                _stateMachine.Transition(new LocomotionState(_stateMachine));
            }
        }
        else if (nextAnimatorState.IsTag("TakeDamage"))
        {
            //Debug.Log("***NextAnimation" + nextAnimatorState.normalizedTime);
            if (nextAnimatorState.normalizedTime >= 1)
            {
                _stateMachine.Transition(new LocomotionState(_stateMachine));
            }
        }
        else
        {
            //Debug.Log("****Not damage animation: " + currentAnimatorState.normalizedTime);
        }
    }

    public override void Exit()
    {
    }
}
