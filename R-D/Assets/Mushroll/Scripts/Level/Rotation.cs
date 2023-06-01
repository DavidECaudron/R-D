using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mushroll
{
    public class Rotation : MonoBehaviour
    {
        // inspector visible

        [Header("Requried")]
        [SerializeField] private Transform _transform;

        [Header("Tweak")]
        [SerializeField] private float _rotationAngle;
        [SerializeField] private float _rotationDuration;

        [Header("Debug")]
        [SerializeField] private bool _isRotating;
        [SerializeField] private bool _isUpsideDown;

        // inspector hidden
        // unity updates
        // main methods
        // coroutine

        private IEnumerator RotationCoroutine()
        {
            float i = 0;
            float currentRotationAngle = _rotationAngle/_rotationDuration;

            Vector3 currentRotation = new Vector3(currentRotationAngle, 0.0f, 0.0f);

            while (i < 180)
            {
                _transform.Rotate(currentRotation * Time.deltaTime, Space.Self);

                i += currentRotation.x * Time.deltaTime;

                yield return new WaitForSeconds(_rotationDuration / currentRotation.x * Time.deltaTime);
            }

            _transform.Rotate(currentRotation * Time.deltaTime, Space.Self);

            yield return new WaitForSeconds(_rotationDuration / currentRotation.x * Time.deltaTime);

            _isUpsideDown = !_isUpsideDown;

            if (_isUpsideDown)
            {
                _transform.rotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);
            }
            else
            {
                _transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }

            //_transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            _isRotating = false;

            StopCoroutine(RotationCoroutine());
        }

        // input system

        public void OnLevel_Rotation(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (!_isRotating)
                {
                    _isRotating = true;

                    StartCoroutine(RotationCoroutine());
                }
            }

            if (context.canceled)
            {

            }
        }
    }
}
