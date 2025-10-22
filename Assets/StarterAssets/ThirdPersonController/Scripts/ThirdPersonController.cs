using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(Rigidbody))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonRigidbodyController : MonoBehaviour
    {
        [Header("Player Movement")]
        public float MoveSpeed = 2.0f;
        public float SprintSpeed = 5.335f;
        public float RotationSmoothTime = 0.12f;
        public float SpeedChangeRate = 10.0f;
        public float JumpForce = 5.0f;
        public float Gravity = -9.81f;
        public LayerMask GroundLayers;
        
        private Rigidbody _rb;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private bool _isGrounded;
        private float _targetRotation;
        private float _rotationVelocity;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _input = GetComponent<StarterAssetsInputs>();
            _mainCamera = Camera.main.gameObject;
        }

        private void FixedUpdate()
        {
            GroundedCheck();
            Move();
        }

        private void GroundedCheck()
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, GroundLayers);
        }

        private void Move()
        {
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            Vector3 velocity = targetDirection.normalized * targetSpeed;
            _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);

            if (_isGrounded && _input.jump)
            {
                _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }
    }
}
