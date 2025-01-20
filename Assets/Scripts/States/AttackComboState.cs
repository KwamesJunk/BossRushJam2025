using UnityEngine;

public class AttackComboState : State
{
    public AttackComboState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _animator.CrossFadeInFixedTime("Sword Attack 3", 0.1f);
        _stateMachine.nextAttackOkay = false;
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedAnimTime() >= 1.0f) {
            _stateMachine.Transition(new LocomotionState(_stateMachine));
        }
        else {
            if (_stateMachine.Reader.IsAttacking && _stateMachine.nextAttackOkay) {
                _animator.CrossFadeInFixedTime("Sword Attack 4", 0.1f);
            }
        }
    }
    public override void Exit()
    {
     
    }

    float GetNormalizedAnimTime()
    {
        AnimatorStateInfo currentInfo = _animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = _animator.GetNextAnimatorStateInfo(0);


        if (_animator.IsInTransition(0) && nextInfo.IsTag("attack")) {
            return _animator.GetNextAnimatorStateInfo(0).normalizedTime;
        }
        else if (currentInfo.IsTag("attack")) {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        return 0;
    }
}
