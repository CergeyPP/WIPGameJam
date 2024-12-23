using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 720.0f;
    [SerializeField] private Animator _animator;
    [Header("Animator properties")]
    [SerializeField] private string _speedPropertyName;
    [SerializeField] private string _jumpTriggerPropertyName;
    [SerializeField] private string _landedTriggerPropertyName;

    private CharacterController _cc;

    private int _speedProperty;
    private int _jumpProperty;
    private int _landedProperty;
    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _speedProperty = Animator.StringToHash(_speedPropertyName);
        _jumpProperty = Animator.StringToHash(_jumpTriggerPropertyName);
        _landedProperty = Animator.StringToHash(_landedTriggerPropertyName);
    }

    private void Update()
    {
        Vector3 velocity = _cc.velocity;
        velocity.y = 0;
        float speed = velocity.magnitude;
        _animator.SetFloat(_speedProperty, speed);
        _animator.SetBool(_landedProperty, _cc.isGrounded);

        if (velocity.magnitude > 0.1)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized, Vector3.up);
            Quaternion currentRotation = transform.rotation;

            transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, _rotationSpeed * Time.deltaTime);

        }
    }

    public void OnJump()
    {
        _animator.SetTrigger(_jumpProperty);
    }

    public void OnLanded()
    {
       
    }
}
