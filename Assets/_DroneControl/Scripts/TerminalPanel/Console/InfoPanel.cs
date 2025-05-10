using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Console
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float waitBetweenLinesInSeconds = 0.1f;
        private List<string> infoLines = new List<string>();
        public const int MAX_LINES = 17;

        private Coroutine currentCoroutine = null;
        private WaitForSeconds waitForSecondsCached;

        public void Start()
        {
            waitForSecondsCached = new WaitForSeconds(waitBetweenLinesInSeconds);
        }

        public void AddLine(string line)
        {
            infoLines.Add(line);
            if (infoLines.Count <= MAX_LINES)
                UpdateTextArea();
        }

        private void Update()
        {
            if (infoLines.Count > MAX_LINES && currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(getNextLine());
            }
        }

        private IEnumerator getNextLine()
        {
            infoLines.RemoveAt(0);
            UpdateTextArea();
            yield return waitForSecondsCached;
            currentCoroutine = null;
        }

        private void UpdateTextArea()
        {
            StringBuilder str = new StringBuilder();
            foreach (var line in infoLines)
            {
                str.AppendLine(line);
            }

            _text.text = str.ToString();
        }
    }
}