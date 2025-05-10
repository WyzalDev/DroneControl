using System;
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

        private const string NO_ITEMS_IN_BUCKET = "<color=\"red\">Nothing To Sell</color>";

        private void Awake()
        {
            EventManager.NoItemsInBucket += NoItemsInBucketInfo;
        }

        private void OnDestroy()
        {
            EventManager.NoItemsInBucket -= NoItemsInBucketInfo;
        }

        private void NoItemsInBucketInfo()
        {
            text.text = NO_ITEMS_IN_BUCKET;
        }

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

        public void Clear()
        {
            text.text = "";
        }
    }
}