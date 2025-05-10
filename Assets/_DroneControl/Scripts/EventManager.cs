using System;

namespace _DroneControl.Scripts
{
    public static class EventManager
    {
        //controls events
        public static event Action ActivatePlayerControl;
        public static event Action DeactivatePlayerControl;
        
        public static event Action ActivatePanelControl;
        public static event Action DeactivatePanelControl;
        
        public static event Action ActivateTextWritingControl;
        public static event Action DeactivateTextWritingControl;
        
        //Level events
        public static event Action NoMoreLevels;
        
        //Commands events
        public static event Action DockedTrue;
        public static event Action DockedFalse;
        public static event Action UndockedTrue;
        public static event Action UndockedFalse;
        public static event Action MoveWhenDontUndock;
        public static event Action DockWhenDocked;
        public static event Action MoveFailedGetDamage;
        public static event Action CollectedItemTrue;
        public static event Action CollectedItemFalse;

        
        //controls events invokes
        public static void InvokeActivatePlayerControl() => ActivatePlayerControl?.Invoke();
        public static void InvokeDeactivatePlayerControl() => DeactivatePlayerControl?.Invoke();
        
        public static void InvokeActivatePanelControl() => ActivatePanelControl?.Invoke();
        public static void InvokeDeactivatePanelControl() => DeactivatePanelControl?.Invoke();
        
        public static void InvokeActivateTextWritingControl() => ActivateTextWritingControl?.Invoke();
        public static void InvokeDeactivateTextWritingControl() => DeactivateTextWritingControl?.Invoke();
        
        //level events invokes
        public static void InvokeNoMoreLevels() => NoMoreLevels?.Invoke();
        
        //Commands events invokes
        public static void InvokeDockedTrue() => DockedTrue?.Invoke();
        public static void InvokeDockedFalse() => DockedFalse?.Invoke();
        public static void InvokeUndockedTrue() => UndockedTrue?.Invoke();
        public static void InvokeUndockedFalse() => UndockedFalse?.Invoke();
        public static void InvokeMoveWhenDontUndock() => MoveWhenDontUndock?.Invoke();
        public static void InvokeDockWhenDocked() => DockWhenDocked?.Invoke();
        public static void InvokeMoveFailedGetDamage() => MoveFailedGetDamage?.Invoke();
        public static void InvokeCollectedItemTrue() => CollectedItemTrue?.Invoke();
        public static void InvokeCollectedItemFalse() => CollectedItemFalse?.Invoke();
        
    }
}