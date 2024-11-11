using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class LandEvent : UnityEvent { }

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [Range(0, 1)]
    [SerializeField] private float _airControl = 0.1f;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private AudioSource source1;
    [SerializeField] private AudioSource source2;
    [SerializeField, Range(0f, 0.5f)] private float volumeCoef;

    [Header("События прыжка/приземления")]
    [Tooltip("Вызывается, когда персонаж совершает прыжок")]
    public LandEvent JumpedEvent;
    [Tooltip("Вызывается, когда персонаж приземляется")]
    public LandEvent LandedEvent;

    private CharacterController _cc;

    private Vector2 _inputMovement = Vector2.zero;

    private Vector3 ForwardMoveVector = Vector3.forward;
    private Vector3 RightMoveVector = Vector3.right;

    private bool _bWantToJump = false;
    private float _timeInJump = 0;
    
    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Camera camera = Camera.main;
        if (camera)
        {
            RightMoveVector = Vector3.Cross(-camera.transform.forward, Vector3.up);
            ForwardMoveVector = Vector3.Cross(RightMoveVector, Vector3.up);
        }

        source1.volume = AudioManager.Instance.GetVolume(volumeCoef);
        source2.volume = AudioManager.Instance.GetVolume(volumeCoef);
        Debug.Log($"s1 volume: {source1.volume}, s2 volume: {source2.volume}");
    }

    public void OnMove(InputValue context)
    {
        _inputMovement = context.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_bWantToJump || (!_cc.isGrounded && _timeInJump > 0))
        {
            if (_bWantToJump)
                JumpedEvent.Invoke();
            MoveInAirByJump();
        }
        else if (!_cc.isGrounded)
        {
            _timeInJump = 0;
            MoveInAirByFall();
        }
        else
        {
            _timeInJump = 0;
            MoveOnGround();
        }

        _bWantToJump = false;
    }

    private void OnJump()
    {
        if (_cc.isGrounded)
        {
            _bWantToJump = true;
        }
    }

    private void MoveInAirByJump()
    {
        float prevTimeInJump = _timeInJump;
        _timeInJump += Time.fixedDeltaTime;
        if (_jumpCurve.keys[_jumpCurve.length - 1].time < _timeInJump)
        {
            _timeInJump = 0;
            MoveInAirByFall();
        }    
        Vector3 horizontalVelocity = CalculateAirHorizontalMovement();
        float prevJumpHeight = _jumpCurve.Evaluate(prevTimeInJump);
        float currentJumpHeight = _jumpCurve.Evaluate(_timeInJump);

        float yTranslation = _jumpHeight * (currentJumpHeight - prevJumpHeight);

        _cc.Move(horizontalVelocity * Time.fixedDeltaTime + yTranslation * Vector3.up);
        if (_cc.isGrounded && (_cc.velocity.y < -0.2))
            LandedEvent.Invoke();
    }
    private void MoveInAirByFall()
    {
        Vector3 horizontalVelocity = CalculateAirHorizontalMovement();
        float yVelocity = _cc.velocity.y;
        yVelocity += Physics.gravity.y * Time.fixedDeltaTime;

        _cc.Move((horizontalVelocity + Vector3.up * yVelocity) * Time.fixedDeltaTime);
        if (_cc.isGrounded && (_cc.velocity.y < -0.2))
            LandedEvent.Invoke();
    }

    private void MoveOnGround()
    {
        Vector3 movementDirection = ForwardMoveVector * _inputMovement.y + RightMoveVector * _inputMovement.x;
        _cc.Move((movementDirection.normalized * _speed + Vector3.down * 0.5f) * Time.fixedDeltaTime);
    }

    private Vector3 CalculateAirHorizontalMovement()
    {
        Vector3 movementDirection = ForwardMoveVector * _inputMovement.y + RightMoveVector * _inputMovement.x;
        Vector3 additionalVelocity = movementDirection * _speed * _airControl;

        Vector3 totalVelocity = _cc.velocity; totalVelocity.y = 0;
        return Vector3.ClampMagnitude(totalVelocity + additionalVelocity, _speed);
    }
}
