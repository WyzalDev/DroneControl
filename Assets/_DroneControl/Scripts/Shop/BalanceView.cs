using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _DroneControl.Scripts.Shop
{
    public class BalanceView : MonoBehaviour
    {
        public TMP_Text balanceText;

        private void Awake()
        {
            EventManager.SelledAllItems += BalanceUpdate;
            EventManager.BatteryBrought += BalanceUpdate;
            EventManager.RepairBrought += BalanceUpdate;
            EventManager.ScannerBrought += BalanceUpdate;
            EventManager.BatteryUpgradeBrought += BalanceUpdate;
            EventManager.LifeUpgradeBrought += BalanceUpdate;
            EventManager.LuckUpgradeBrought += BalanceUpdate;
            EventManager.NotEnoughMoney += BalanceAnimationWhenNoMoney;
            BalanceUpdate();
        }

        private void OnDestroy()
        {
            EventManager.SelledAllItems -= BalanceUpdate;
            EventManager.BatteryBrought -= BalanceUpdate;
            EventManager.RepairBrought -= BalanceUpdate;
            EventManager.ScannerBrought -= BalanceUpdate;
            EventManager.BatteryUpgradeBrought -= BalanceUpdate;
            EventManager.LifeUpgradeBrought -= BalanceUpdate;
            EventManager.LuckUpgradeBrought -= BalanceUpdate;
            EventManager.NotEnoughMoney -= BalanceAnimationWhenNoMoney;
        }

        private void BalanceUpdate()
        {
            balanceText.text = ShopStorage.Balance.ToString();
        }

        private void BalanceAnimationWhenNoMoney()
        {
            var balanceColor = balanceText.color;
            var sequence = DOTween.Sequence();
            balanceText.DOColor(Color.red, 0.3f).SetEase(Ease.InOutQuart).OnComplete(() =>
                balanceText.DOColor(balanceColor, 0.3f).SetEase(Ease.InOutQuart));
        }
    }
}