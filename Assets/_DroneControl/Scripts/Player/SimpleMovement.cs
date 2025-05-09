using _DroneControl.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _DroneControl.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class SimpleMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        
        private Rigidbody _rigidbody;
        private Vector3 _inputKey;
        private InputAction moveAction;

        public static bool isBlocked { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            EventManager.ActivatePlayerControl += Unblock;
            EventManager.DeactivatePlayerControl += Block;
        }

        private void Start()
        {
            moveAction = InputSystem.actions.FindAction("Move");
        }

        private void Update()
        {
            var moveValue = moveAction.ReadValue<Vector2>();
            _inputKey = new Vector3(moveValue.x, 0, moveValue.y);
        }

        private void FixedUpdate()
        {
            if (!isBlocked)
            {
                var move = transform.right * _inputKey.x +  transform.forward * _inputKey.z;
                _rigidbody.MovePosition(transform.position + move * _speed * Time.fixedDeltaTime);
            }
        }

        private void OnDestroy()
        {
            EventManager.ActivatePlayerControl -= Unblock;
            EventManager.DeactivatePlayerControl -= Block;
        }
        
        private static void Block()
        {
            isBlocked = true;
        }

        private static void Unblock()
        {
            isBlocked = false;
        }
    }
}