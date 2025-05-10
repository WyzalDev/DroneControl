using System;
using System.Collections.Generic;
using _DroneControl.Scripts;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Minigame
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private List<Level> levels;
        [SerializeField] private int startLevel;
        
        [NonSerialized] private int currentLevel;
        
        public static LevelHandler Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            currentLevel = startLevel;
        }

        public static Level GetLevel()
        {
            if (Instance.currentLevel < Instance.levels.Count)
            {
                var result = Instance.levels[Instance.currentLevel];
                Instance.currentLevel++;
                return result;
            }
            else
            {
                EventManager.InvokeNoMoreLevels();
                return null;
            }
            
        }
    }
}