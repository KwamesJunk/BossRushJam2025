using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LolaRun : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _maxRunSpeed;
    [SerializeField] float _maxWalkSpeed;
    [SerializeField] Vector3 _verticalVelocity;
    Vector3 _horizontalDirection;
    [SerializeField] float _acceleration;
    [SerializeField] float _brakingAcceleration;
    [SerializeField] InputReader _reader;
    [SerializeField] CharacterController _controller;
    [SerializeField] Animator _animator;
    [SerializeField] float _gravity;
    [SerializeField] float _jumpVelocity;
    [SerializeField] float SPEEDRATIO;
    [SerializeField] AttackCombo _attackCombo;

    bool _jumping;
    float _maxSpeed;
    int comboIndex;

    // Start is called before the first frame update
    void Start()
    {
        _horizontalDirection = transform.forward;
        _reader.OnJump += Jump;
        _reader.OnWalkStarted += StartWalk;
        _reader.OnWalkFinished += StopWalk;
        _reader.OnAttack += Attack;

        _speed = 0;
        _maxSpeed = _maxRunSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputDirection = Vector3.zero;
        Vector2 movementInput = _reader.Movement;

        if (!_jumping)
        {
            if (movementInput != Vector2.zero)
            {
                inputDirection = (movementInput.x * Camera.main.transform.right) + (movementInput.y * Camera.main.transform.forward);
                //_speed += _acceleration * Time.deltaTime;
                inputDirection.y = 0;
                inputDirection.Normalize();
                transform.rotation = Quaternion.LookRotation(inputDirection);

                _horizontalDirection = inputDirection;
            }
            //else
            //{
            //    _speed += _brakingAcceleration * Time.deltaTime;
            //}

            //if (_speed > 0.01f)
            //{
            //    if (_speed > _maxSpeed) _speed += _brakingAcceleration * Time.deltaTime;

            //}
            //else
            //{
            //    _speed = 0;
            //}

            _speed = _reader.Movement.magnitude * _maxRunSpeed;
            if (_speed > _maxSpeed) _speed = _maxSpeed;
        }

        _verticalVelocity += _gravity * Time.deltaTime * transform.up;
        if (_controller.isGrounded && _verticalVelocity.y < 0)
        {
            _verticalVelocity = Vector3.zero;

            if (_jumping)  _speed = 0;
            _jumping = false;
        }

        Vector3 horizontalVelocity = _horizontalDirection * _speed;
        _controller.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
        _animator.SetBool("Is Grounded", _controller.isGrounded);
        _animator.SetFloat("Speed", _speed/_maxRunSpeed);
        SPEEDRATIO = _speed / _maxRunSpeed;
    }

    void Jump()
    {
        if (!_jumping)
        {
            Debug.Log("Jump!");
            _animator.Play("Start Jump");
            _verticalVelocity = new Vector3(0, _jumpVelocity, 0);
            _jumping = true;
        }
    }

    void StartWalk()
    {
        Debug.Log("Walk");
        _maxSpeed = _maxWalkSpeed;
    }

    void StopWalk()
    {
        Debug.Log("Run");
        _maxSpeed = _maxRunSpeed;
    }

    void Attack()
    {
        Debug.Log("Attack!!");

        AnimatorStateInfo currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextAnimatorStateInfo = _animator.GetNextAnimatorStateInfo(0);
        AnimatorClipInfo[] currentAnimatorClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        AnimatorClipInfo[] nextAnimatorClipInfo = _animator.GetNextAnimatorClipInfo(0);
        
        if (_animator.IsInTransition(0))
        {
            if (nextAnimatorStateInfo.normalizedTime * nextAnimatorClipInfo.Length < _attackCombo.Attacks[comboIndex].minDuration) return;
            
            //StartNextAttack
        }
        //if (animatorStateInfo.f)

        _animator.CrossFadeInFixedTime("Attack 1", 0.1f);
    }
}
