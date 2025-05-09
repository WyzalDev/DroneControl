using System;

namespace _DroneControl.Scripts
{
    public static class EventManager
    {
        public static event Action ActivatePlayerControl;
        public static event Action DeactivatePlayerControl;
        
        public static event Action ActivatePanelControl;
        public static event Action DeactivatePanelControl;
        
        public static void InvokeActivatePlayerControl() => ActivatePlayerControl?.Invoke();
        public static void InvokeDeactivatePlayerControl() => DeactivatePlayerControl?.Invoke();
        
        public static void InvokeActivatePanelControl() => ActivatePanelControl?.Invoke();
        public static void InvokeDeactivatePanelControl() => DeactivatePanelControl?.Invoke();
    }
}