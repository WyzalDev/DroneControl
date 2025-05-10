using System;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Minigame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        [NonSerialized] public SpriteRenderer spriteRenderer;
        [NonSerialized] public CellType cellType;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
    
    public enum CellType {
        Empty = 0,
        Obstacle = 1,
        Player = 2,
        PlayerOnLoot = 3,
        Loot = 4,
        Shop = 5
    }
}