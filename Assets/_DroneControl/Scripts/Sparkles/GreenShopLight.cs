using System;
using UnityEngine;

namespace _DroneControl.Scripts.Sparkles
{
    public class GreenShopLight : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.DockedTrue += Show;
            EventManager.UndockedTrue += Hide;
            EventManager.MoveWhenDontUndock += Hide;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            EventManager.DockedTrue -= Show;
            EventManager.UndockedTrue -= Hide;
            EventManager.MoveWhenDontUndock -= Hide;
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}