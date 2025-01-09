using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : MonoBehaviour
{
    public InputActionAsset _inputActions;
    public event Action OnJump;
    public event Action OnWalkStarted;
    public event Action OnWalkFinished;
    public event Action OnAttack;
    InputAction _moveAction;
    InputAction _walkAction;
    InputAction _attackAction;

    public Vector2 Movement => _moveAction.ReadValue<Vector2>();
    public bool IsWalking => _walkAction.IsPressed();
    public bool IsAttacking => _attackAction.IsPressed();

    
    void Awake ()
    {
        _moveAction = _inputActions.FindActionMap("Standard").FindAction("Movement");
        _inputActions.FindActionMap("Standard").FindAction("Jump").performed += JumpPerformed;
        
        _walkAction = _inputActions.FindActionMap("Standard").FindAction("Walk");
        _walkAction.performed += WalkStarted;
        _walkAction.canceled += WalkFinished;
        
        _attackAction = _inputActions.FindActionMap("Standard").FindAction("Attack");
        _attackAction.performed += AttackPerformed;
    }

    void JumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    void WalkStarted(InputAction.CallbackContext context)
    {
        OnWalkStarted?.Invoke();
    }

    void WalkFinished(InputAction.CallbackContext context)
    {
        OnWalkFinished?.Invoke();
    }

    private void OnEnable()
    {
        _inputActions.FindActionMap("Standard").Enable();
    }

    private void OnDisable()
    {
        _inputActions.FindActionMap("Standard").Disable();
    }

    private void AttackPerformed(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }
}
