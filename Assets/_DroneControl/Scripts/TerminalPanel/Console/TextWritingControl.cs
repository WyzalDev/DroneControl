using System;
using _DroneControl.Scripts;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Console
{
    public class TextWritingControl : MonoBehaviour
    {

        public void Select(string str)
        {
            EventManager.InvokeActivateTextWritingControl();
        }

        public void Deselect(string str)
        {
            EventManager.InvokeDeactivateTextWritingControl();
        }
        
    }
}