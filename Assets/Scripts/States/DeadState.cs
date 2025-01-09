using UnityEngine;

public class DeadState : State
{
    public DeadState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {Debug.Log("Dead state");
        _animator.CrossFadeInFixedTime("Dead", 0.1f);
    }

      public override void Tick(float deltaTime)
    {
        _stateMachine.MoveCharacter();
    }

    public override void Exit()
    {
        
    }
}
