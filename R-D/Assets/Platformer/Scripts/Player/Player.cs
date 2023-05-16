using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer
{
    public class Player : MonoBehaviour
    {
        // inspector visible

        [Header("Required")]
        [SerializeField] private CharacterController _characterController;

        [Header("Tweak")]
        [SerializeField] private float _gravityValue;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpHeight;

        [Header("Debug")]
        [SerializeField] private bool _isMoving;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isJumping;

        // inspector hidden

        private Vector3 _movement = new (0.0f, 0.0f, 0.0f);

        private float deltaTime = 0.0f;
        private float _movementX = 0.0f;
        private float _jump = 0.0f;

        // unity updates

        private void FixedUpdate()
        {
            deltaTime = Time.fixedDeltaTime;

            Movement();
        }

        // main methods

        private void Movement()
        {
            _movement.x = _movementSpeed * _movementX;

            if (_movement.x != 0.0f)
            {
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }

            if (_characterController.isGrounded)
            {
                _movement.y = 0.0f;

                _isGrounded = true;
                _isJumping = false;
            }
            else
            {
                _isGrounded = false;
            }

            if (_jump != 0.0f && _isGrounded)
            {
                _movement.y += Mathf.Sqrt(_jumpHeight * _gravityValue * 2.0f);
                _isJumping = true;
            }
            else
            {
                _movement.y -= _gravityValue * deltaTime;
            }

            _characterController.Move(_movement * deltaTime);
        }

        // input action methods

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 temp = context.ReadValue<Vector2>();

                _movementX = temp.x;
            }

            if (context.canceled)
            {
                Vector2 temp = context.ReadValue<Vector2>();

                _movementX = temp.x;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _jump = context.ReadValue<float>();
            }

            if (context.canceled)
            {
                _jump = context.ReadValue<float>();
            }
        }
    }
}
