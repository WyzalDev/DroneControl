using System;
using System.Collections.Generic;
using _DroneControl.Scripts.Shop.SellBucket;

namespace _DroneControl.Scripts
{
    public static class EventManager
    {
        //controls events
        public static event Action ActivatePlayerControl;
        public static event Action DeactivatePlayerControl;
        
        public static event Action ActivatePanelControl;
        public static event Action DeactivatePanelControl;
        
        public static event Action ActivateShopControl;
        public static event Action DeactivateShopControl;
        
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
        
        //Shop events
        public static event Action SellItemsInBucket;
        public static event Action<List<PhysicItem>> SellAllItems;
        public static event Action SelledAllItems;
        public static event Action NotEnoughMoney;
        public static event Action BatteryBrought; 
        public static event Action RepairBrought; 
        public static event Action ScannerBrought;
        public static event Action BatteryUpgradeBrought;
        public static event Action LuckUpgradeBrought;
        public static event Action LifeUpgradeBrought;
        
        //Bucket events
        public static event Action NoItemsInBucket;
        
        //Game control events
        public static event Action<bool> EndGame;
        
        //Battery events
        public static event Action OneBatteryUsed;
        public static event Action MaxCapacityWhenBuy;
        
        //controls events invokes
        public static void InvokeActivatePlayerControl() => ActivatePlayerControl?.Invoke();
        public static void InvokeDeactivatePlayerControl() => DeactivatePlayerControl?.Invoke();
        
        public static void InvokeActivatePanelControl() => ActivatePanelControl?.Invoke();
        public static void InvokeDeactivatePanelControl() => DeactivatePanelControl?.Invoke();
        
        public static void InvokeActivateShopControl() => ActivateShopControl?.Invoke();
        public static void InvokeDeactivateShopControl() => DeactivateShopControl?.Invoke();
        
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
        
        //Shop events invokes
        public static void InvokeSellItemsInBucket() => SellItemsInBucket?.Invoke();
        public static void InvokeSellAllItems(List<PhysicItem> items) => SellAllItems?.Invoke(items); 
        public static void InvokeSelledAllItems() => SelledAllItems?.Invoke();
        public static void InvokeNotEnoughMoney() => NotEnoughMoney?.Invoke();

        public static void InvokeBatteryBrought() => BatteryBrought?.Invoke();
        public static void InvokeRepairBrought() => RepairBrought?.Invoke();
        public static void InvokeScannerBrought() => ScannerBrought?.Invoke();
        public static void InvokeBatteryUpgradeBrought() => BatteryUpgradeBrought?.Invoke();
        public static void InvokeLifeUpgradeBrought() => LifeUpgradeBrought?.Invoke();
        public static void InvokeLuckUpgradeBrought() => LuckUpgradeBrought?.Invoke();
        
        //Bucket events invokes
        public static void InvokeNoItemsInBucket() => NoItemsInBucket?.Invoke();
        
        //Game control events invokes
        public static void InvokeEndGame(bool flag) => EndGame?.Invoke(flag);
        
        //Battery events invokes
        public static void InvokeOneBatteryUsed() => OneBatteryUsed?.Invoke();
        public static void InvokeMaxCapacityWhenBuy() => MaxCapacityWhenBuy?.Invoke();
    }
}