using System;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Minigame
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "WyzalUtilities/Minigame/Level")]
    public class Level : ScriptableObject
    {
        [SerializeField] public LineGrid[] table;
    }

    [Serializable]
    public class LineGrid
    {
        [SerializeField] public CellType[] line;
    }
}