using UnityEngine;

public abstract class State
{
    protected PlayerStateMachine _stateMachine;
    protected Animator _animator;
    protected LifeStats _lifeStats;
    protected string _name;

    public string Name => _name;

    public State(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animator = stateMachine.CharAnimator;
        _lifeStats = stateMachine.Life;
    }

    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();

    
}
