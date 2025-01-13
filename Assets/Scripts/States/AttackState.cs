using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    float _timekeeper;
    int _comboIndex;
    List<AttackCombo.AttackInfo> _comboAttacks;

    public AttackState(StateMachine stateMachine) : base(stateMachine)
    {
        _comboAttacks = stateMachine.BasicCombo.Attacks;
    }

    public override void Enter()
    {
        _comboIndex = 0;
        _timekeeper = 0;
        //_stateMachine.Reader.OnAttack += NextComboAttack;
        _animator.CrossFadeInFixedTime(_comboAttacks[0].animationName, 0.1f);
        //_stateMachine.Weapon.enabled = true;
    }

    public override void Tick(float deltaTime)
    {
        _stateMachine.MoveCharacter();
        _timekeeper += deltaTime;
        float normalizedTime = GetNormalizedAnimTime();

        //Debug.Log($"{GetAnimName().name}: {GetNormalizedAnimTime()}");

        if (normalizedTime > _comboAttacks[_comboIndex].minDuration) 
        {
            //_stateMachine.Weapon.enabled = false;// TODO: move this to the animation
            if (_stateMachine.Reader.IsAttacking) {
                NextComboAttack();
            }
            else {
                if (_stateMachine.IsMoving) {
                    _stateMachine.Transition(new LocomotionState(_stateMachine));
                    return;
                }
            }
        }

        // if (normalizedTime > 0.3f) { // time before ability to cancel attack
        //     if (!_stateMachine.Reader.IsAttacking) {
        //          
        //     }
        // }
        if (normalizedTime >= 1.0f)
        {
            _stateMachine.Transition(new LocomotionState(_stateMachine));
        }
    }

    public override void Exit()
    {
        //_stateMachine.Reader.OnAttack -= NextComboAttack;
        //_stateMachine.Weapon.enabled = false;
    }

    float GetNormalizedAnimTime()
    {
        AnimatorStateInfo currentInfo = _animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = _animator.GetNextAnimatorStateInfo(0);


        if (_animator.IsInTransition(0) && nextInfo.IsTag("attack")) {
            return _animator.GetNextAnimatorStateInfo(0).normalizedTime;
        }
        else if (currentInfo.IsTag("attack"))
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        return 0;
    }

    AnimationClip GetAnimName()
    {
        if (_animator.IsInTransition(0)) return _animator.GetNextAnimatorClipInfo(0)[0].clip;

        return _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
    }

    private void NextComboAttack()
    {
        if (_comboIndex >= _comboAttacks.Count - 1) return;

        if (GetNormalizedAnimTime() < _comboAttacks[_comboIndex].minDuration) return;

        ++_comboIndex;
        _timekeeper = 0;
        _animator.CrossFadeInFixedTime(_comboAttacks[_comboIndex].animationName, 0.1f);
    }
}