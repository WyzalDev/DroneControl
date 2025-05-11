using System.Collections.Generic;
using _DroneControl.Scripts;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Minigame
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private List<Level> levels;
        [SerializeField] private int startLevel;
        
        private int currentLevel;
        
        private void Awake()
        {
            currentLevel = startLevel;
        }

        public Level GetLevel()
        {
            if (currentLevel < levels.Count)
            {
                var result = levels[currentLevel];
                currentLevel++;
                return result;
            }
            else
            {
                return null;
            }
            
        }
    }
}