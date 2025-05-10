using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace _DroneControl.Scripts.Shop
{
    public class ShopInfoHandler : MonoBehaviour
    {
        public TMP_Text text;
        private List<string> lines = new List<string>();

        public void AddLine(string line)
        {
            lines.Add(line);
        }

        public void Submit()
        {
            var builder = new StringBuilder();
            foreach (var line in lines)
            {
                builder.AppendLine(line);
            }
            text.text = builder.ToString();
            lines.Clear();
        }
    }
}