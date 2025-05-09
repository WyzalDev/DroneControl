using System;
using _DroneControl.Player;
using _DroneControl.Scripts;
using UnityEngine;

namespace _DroneControl.TerminalPanel
{
    public class InteractableTerminal : MonoBehaviour, IInteractable
    {
        [SerializeField] private string tooltip = "DefaultTooltip";

        public string TooltipMessage { get; set; }

        public static bool isBlocked = false;

        private void Awake()
        {
            TooltipMessage = tooltip;
            EventManager.ActivatePlayerControl += Unblock;
            EventManager.DeactivatePlayerControl += Block;
        }

        public void Interact()
        {
            if (!isBlocked)
            {
                EventManager.InvokeDeactivatePlayerControl();
                EventManager.InvokeActivatePanelControl();
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