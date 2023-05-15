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

        [Header("Debug")]
        [SerializeField] private bool _isMoving;
        [SerializeField] private bool _isGrounded;

        // inspector hidden

        private Vector3 _movement = new (0.0f, 0.0f, 0.0f);

        private float deltaTime = 0.0f;
        private float _movementX = 0.0f;

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
            }
            else
            {
                _isGrounded = false;
            }

            _movement.y -= _gravityValue * deltaTime;

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
    }
}
