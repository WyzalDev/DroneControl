using _DroneControl.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _DroneControl.Player
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private Transform playerBody;
        private float xRotation = 0f;
        private InputAction _mouseInput;
        
        public static bool isBlocked { get; private set; }

        private void Awake()
        {
            EventManager.ActivatePlayerControl += Unblock;
            EventManager.DeactivatePlayerControl += Block;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _mouseInput = InputSystem.actions.FindAction("Look");
        }

        void Update()
        {
            if (!isBlocked)
            {
                var mouseInput = _mouseInput.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseInput.y;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseInput.x);
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