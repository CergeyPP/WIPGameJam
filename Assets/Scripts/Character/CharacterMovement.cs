using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private AnimationCurve _jumpCurve;

    private CharacterController _cc;
    private PlayerInput _input;

    private Vector2 _inputMovement = Vector2.zero;

    private Vector3 ForwardMoveVector = Vector3.forward;
    private Vector3 RightMoveVector = Vector3.right;

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
    }

    public void OnMove(InputValue context)
    {
        _inputMovement = context.Get<Vector2>();
    }
    
    private void FixedUpdate()
    {
        Vector3 movementDirection = ForwardMoveVector * _inputMovement.y + RightMoveVector * _inputMovement.x;
        _cc.Move(movementDirection.normalized * _speed * Time.fixedDeltaTime);
    }
}
