using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    [SerializeField] protected string _currentStateName;
    [SerializeField] public CharacterController CharController;
    [SerializeField] public Animator CharAnimator;
    [SerializeField] public LifeStats Life;

    protected State _currentState;
}