using _DroneControl.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _DroneControl.TerminalPanel
{
    public class TerminalPanelControlSwitcher : MonoBehaviour
    {

        public static bool isBlocked = true;

        private InputAction _closePanelAction;

        private void Awake()
        {
            EventManager.ActivatePanelControl += Unblock;
            EventManager.DeactivatePanelControl += Block;
        }

        private void Start()
        {
            _closePanelAction = InputSystem.actions.FindAction("ClosePanel");
        }

        private void Update()
        {
            if (!isBlocked)
            {
                if (_closePanelAction.IsPressed())
                {
                    ControlToPlayer();
                }
            }
        }
        
        public void ControlToPlayer()
        {
            EventManager.InvokeDeactivatePanelControl();
            EventManager.InvokeActivatePlayerControl();
        }

        private void OnDestroy()
        {
            EventManager.ActivatePanelControl -= Unblock;
            EventManager.DeactivatePanelControl -= Block;
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