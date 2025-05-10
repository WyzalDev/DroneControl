using System.Collections.Generic;
using _DroneControl.Scripts;
using _DroneControl.TerminalPanel.Console;
using Unity.VisualScripting;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Minigame
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int height;
        [SerializeField] private int width;

        [SerializeField] private Tile tile;

        [Header("Sprites Settings")]
        [SerializeField] private Sprite emptyCell;
        [SerializeField] private Sprite obstacleCell;
        [SerializeField] private Sprite playerCell;
        [SerializeField] private Sprite playerOnLootCell;
        [SerializeField] private Sprite lootCell;
        [SerializeField] private Sprite shopCell;
        

        public static GridManager Instance;
        public Tile[,] gridArray;

        //player functionality
        private int _playerX;
        private int _playerY;
        private bool isDocked;

        private Level _currentLevel;
        private Transform parent;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                EventManager.DockedTrue += NextLevel;
                EventManager.DockedTrue += HideGrid;
                EventManager.UndockedTrue += ShowGrid;
                EventManager.MoveWhenDontUndock += ShowGrid;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            parent = new GameObject("Grid Parent").transform;
            parent.SetParent(transform);
            parent.localPosition = Vector3.zero;

            gridArray = new Tile[width, height];

            GenerateGrid();
            _currentLevel = LevelHandler.GetLevel();
            if (_currentLevel != null)
                FillLevel();
        }

        private void OnDestroy()
        {
            EventManager.DockedTrue -= NextLevel;
            EventManager.DockedTrue -= HideGrid;
            EventManager.UndockedTrue -= ShowGrid;
            EventManager.MoveWhenDontUndock -= ShowGrid;
        }

        #region Grid

        public static void ShowGrid()
        {
            if (Instance._currentLevel != null)
                Instance.parent.gameObject.SetActive(true);
        }

        public static void HideGrid()
        {
            Instance.parent.gameObject.SetActive(false);
        }

        private void NextLevel()
        {
            _currentLevel = LevelHandler.GetLevel();
            if (_currentLevel != null)
            {
                FillLevel();
            }
            else
            {
                return;
            }
        }

        private void FillLevel()
        {
            ClearLevel();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var levelCell = _currentLevel.table[y].line[x];
                    if (levelCell != gridArray[x, y].cellType)
                    {
                        switch (levelCell)
                        {
                            case CellType.Player:
                                gridArray[x, y].cellType = CellType.Player;
                                gridArray[x, y].spriteRenderer.sprite = playerCell;
                                _playerX = x;
                                _playerY = y;
                                break;
                            case CellType.Loot:
                                gridArray[x, y].cellType = CellType.Loot;
                                gridArray[x, y].spriteRenderer.sprite = lootCell;
                                break;
                            case CellType.Obstacle:
                                gridArray[x, y].cellType = CellType.Obstacle;
                                gridArray[x, y].spriteRenderer.sprite = obstacleCell;
                                break;
                            case CellType.Empty:
                            default:
                                gridArray[x, y].cellType = CellType.Empty;
                                gridArray[x, y].spriteRenderer.sprite = emptyCell;
                                break;
                        }
                    }
                }
            }
        }

        private void ClearLevel()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var cell = gridArray[x, y];
                    if (cell.cellType != CellType.Empty)
                    {
                        cell.cellType = CellType.Empty;
                        cell.spriteRenderer.sprite = emptyCell;
                    }
                }
            }
        }

        private void GenerateGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var spawnedTile = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.transform.parent = parent;
                    spawnedTile.cellType = CellType.Empty;
                    Debug.Log($"{x}, {y}");
                    gridArray[x, y] = spawnedTile;
                }
            }

            parent.localScale = new Vector3(0.245f, 0.245f, 1);
            parent.localPosition = new Vector3(-0.475f, 1.18f, 9.81f);
        }

        #endregion

        #region PlayerCommands

        public static void HandleAllShipCommands(List<ShipCommand> shipCommands)
        {
            foreach (var command in shipCommands)
            {
                switch (command)
                {
                    case ShipCommand.MoveUp:
                        Instance.HandleMoveUpCommand();
                        break;
                    case ShipCommand.MoveDown:
                        Instance.HandleMoveDownCommand();
                        break;
                    case ShipCommand.MoveLeft:
                        Instance.HandleMoveLeftCommand();
                        break;
                    case ShipCommand.MoveRight:
                        Instance.HandleMoveRightCommand();
                        break;
                    case ShipCommand.Dock:
                        Instance.HandleDockCommand();
                        break;
                    case ShipCommand.UnDock:
                        Instance.HandleUndockCommand();
                        break;
                    case ShipCommand.Collect:
                        Instance.HandleCollectCommand();
                        break;
                }
            }
        }

        private void HandleMoveUpCommand()
        {
            if (isDocked)
            {
                EventManager.InvokeMoveWhenDontUndock();
                isDocked = false;
            }
            if (_playerY + 1 == height || gridArray[_playerX, _playerY + 1].cellType == CellType.Obstacle)
            {
                EventManager.InvokeMoveFailedGetDamage();
            }
            else
            {
                MovePlayerTo(_playerX, _playerY + 1);
            }
        }

        private void HandleMoveDownCommand()
        {
            if (isDocked)
            {
                EventManager.InvokeMoveWhenDontUndock();
                isDocked = false;
            }
            if (_playerY - 1 == -1 || gridArray[_playerX, _playerY - 1].cellType == CellType.Obstacle)
            {
                EventManager.InvokeMoveFailedGetDamage();
            }
            else
            {
                MovePlayerTo(_playerX, _playerY - 1);
            }
        }

        private void HandleMoveRightCommand()
        {
            if (isDocked)
            {
                EventManager.InvokeMoveWhenDontUndock();
                isDocked = false;
            }
            if (_playerX + 1 == width || gridArray[_playerX + 1, _playerY].cellType == CellType.Obstacle)
            {
                EventManager.InvokeMoveFailedGetDamage();
            }
            else
            {
                MovePlayerTo(_playerX + 1, _playerY);
            }
        }

        private void HandleMoveLeftCommand()
        {
            if (isDocked)
            {
                EventManager.InvokeMoveWhenDontUndock();
                isDocked = false;
            }
            if (_playerX - 1 == -1 || gridArray[_playerX - 1, _playerY].cellType == CellType.Obstacle)
            {
                EventManager.InvokeMoveFailedGetDamage();
            }
            else
            {
                MovePlayerTo(_playerX - 1, _playerY);
            }
        }

        private void MovePlayerTo(int x, int y)
        {
            switch (gridArray[_playerX, _playerY].cellType)
            {
                case CellType.Player:
                    gridArray[_playerX, _playerY].cellType = CellType.Empty;
                    gridArray[_playerX, _playerY].spriteRenderer.sprite = emptyCell;
                    break;
                case CellType.PlayerOnLoot:
                    gridArray[_playerX, _playerY].cellType = CellType.Loot;
                    gridArray[_playerX, _playerY].spriteRenderer.sprite = lootCell;
                    break;
            }

            _playerX = x;
            _playerY = y;

            switch (gridArray[_playerX, _playerY].cellType)
            {
                case CellType.Empty:
                    gridArray[_playerX, _playerY].cellType = CellType.Player;
                    gridArray[_playerX, _playerY].spriteRenderer.sprite = playerCell;
                    break;
                case CellType.Loot:
                    gridArray[_playerX, _playerY].cellType = CellType.PlayerOnLoot;
                    gridArray[_playerX, _playerY].spriteRenderer.sprite = playerOnLootCell;
                    break;
            }
        }

        private void HandleDockCommand()
        {
            if (isDocked)
            {
                EventManager.InvokeDockWhenDocked();
            }

            if (gridArray[_playerX, _playerY].cellType != CellType.Shop)
            {
                EventManager.InvokeDockedFalse();
            }

            if (gridArray[_playerX, _playerY].cellType == CellType.Shop)
            {
                isDocked = true;
                EventManager.InvokeDockedTrue();
            }
        }

        private void HandleUndockCommand()
        {
            if (isDocked)
            {
                isDocked = false;
                EventManager.InvokeUndockedTrue();
            }

            {
                EventManager.InvokeUndockedFalse();
            }
        }

        private void HandleCollectCommand()
        {
            if (gridArray[_playerX, _playerY].cellType == CellType.PlayerOnLoot)
            {
                EventManager.InvokeCollectedItemTrue();
            }
            else
            {
                EventManager.InvokeCollectedItemFalse();
            }
        }

        #endregion
    }
}