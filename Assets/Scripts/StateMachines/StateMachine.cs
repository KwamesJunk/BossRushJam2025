using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent (typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(LifeStats))]
[RequireComponent(typeof(Rigidbody))]
public abstract class StateMachine : MonoBehaviour
{
    [SerializeField] protected string _currentStateName;
    [SerializeField] public HitBox meleeHitBox;
    public CharacterController charController;
    public Animator charAnimator;
    public LifeStats lifeStats;

    protected State _currentState;

    protected void InitializeComponents()
    {
        charController = GetComponent<CharacterController>();
        charAnimator = GetComponent<Animator>();
        lifeStats = GetComponent<LifeStats>();
    }

    public void Transition(State newState)
    {
        Debug.Log($"Transition from {_currentState} to {newState}");
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();

        _currentStateName = _currentState.Name;
    }

    public void SetMeleeHitBoxPositionAndSize(Vector3 localPosition, Vector3 size)
    {
        meleeHitBox.transform.localPosition = localPosition;
        meleeHitBox.transform.localScale = size;
    }
}