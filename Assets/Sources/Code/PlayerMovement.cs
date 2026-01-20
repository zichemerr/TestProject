using System;
using UnityEngine;

namespace Sources.Code
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _jumpForce = 5;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _runningSpeed;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _returnSpeed;
        
        private PlayerInput _input;
        private float _currentRotation;
        private Vector2 _endPosition;
        private bool _isGrounded = false;
        
        public void Init(PlayerInput playerInput)
        {
            _input = playerInput;
            _input.Jumped += OnJumped;
        }
        
        private void OnDisable()
        {
            _input.Jumped -= OnJumped;
        }

        private void FixedUpdate()
        {
            if (_input.IsActive == false)
                return;
            
            var horizontal = _input.Horizontal;
            var vertical = _input.Vertical;
            
            Vector3 horizontalVelocity = transform.forward * _runningSpeed;
            _rigidbody.linearVelocity = new Vector3(horizontalVelocity.x, _rigidbody.linearVelocity.y, vertical * _runningSpeed);
    
            if (horizontal != 0)
            {
                float targetRotation = _currentRotation + horizontal * _rotationSpeed;
                targetRotation = Mathf.Clamp(targetRotation, -_maxAngle, _maxAngle);
                _currentRotation = targetRotation;
            }
            else
            {
                _currentRotation = Mathf.Lerp(_currentRotation, 0f, _returnSpeed);
            }
    
            transform.rotation = Quaternion.Euler(0, _currentRotation, 0);
        }

        private void OnCollisionEnter(Collision other)
        {
            _isGrounded = true;
        }

        private void OnJumped()
        {
            if (_isGrounded == false)
                return;

            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }

        public void Disable()
        {
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }
}