using System.Collections.Generic;
using _DroneControl.Scripts;
using _DroneControl.TerminalPanel.Console;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Minigame
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int height;
        [SerializeField] private int width;

        [SerializeField] private Tile tile;
        [SerializeField] private LevelHandler levelHandler;

        [Header("Sprites Settings")]
        [SerializeField] private Sprite emptyCell;
        [SerializeField] private Sprite obstacleCell;
        [SerializeField] private Sprite playerCell;
        [SerializeField] private Sprite playerOnLootCell;
        [SerializeField] private Sprite lootCell;
        [SerializeField] private Sprite shopCell;
        [SerializeField] private Sprite playerOnShopCell;
        

        public static GridManager Instance;
        public Tile[,] gridArray;

        //player functionality
        private int _playerX;
        private int _playerY;
        public static bool isDocked;

        private Level _currentLevel;
        private Transform _parent;

        private void Awake()
        {
            EventManager.DockedTrue += NextLevel;
            EventManager.UndockedTrue += ShowGrid;
            EventManager.MoveWhenDontUndock += ShowGrid;
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _parent = new GameObject("Grid Parent").transform;
            _parent.SetParent(transform);
            _parent.localPosition = Vector3.zero;

            gridArray = new Tile[width, height];

            GenerateGrid();
            _currentLevel = levelHandler.GetLevel();
            if (_currentLevel != null)
                FillLevel();
        }

        private void OnDestroy()
        {
            EventManager.DockedTrue -= NextLevel;
            EventManager.UndockedTrue -= ShowGrid;
            EventManager.MoveWhenDontUndock -= ShowGrid;
        }

        #region Grid

        public static void ShowGrid()
        {
            if (Instance._currentLevel != null)
                Instance._parent.gameObject.SetActive(true);
        }

        public static void HideGrid()
        {
            Instance._parent.gameObject.SetActive(false);
        }

        private void NextLevel()
        {
            _currentLevel = levelHandler.GetLevel();
            if (_currentLevel != null)
            {
                FillLevel();
            }
            else
            {
                EventManager.InvokeEndGame(true);
            }
            HideGrid();
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
                                gridArray[x, y].spriteRenderer.color = Color.green;
                                _playerX = x;
                                _playerY = y;
                                break;
                            case CellType.Loot:
                                gridArray[x, y].cellType = CellType.Loot;
                                gridArray[x, y].spriteRenderer.sprite = lootCell;
                                gridArray[x, y].spriteRenderer.color = Color.yellow;
                                break;
                            case CellType.Obstacle:
                                gridArray[x, y].cellType = CellType.Obstacle;
                                gridArray[x, y].spriteRenderer.sprite = obstacleCell;
                                gridArray[x, y].spriteRenderer.color = Color.white;
                                break;
                            case CellType.Shop:
                                gridArray[x, y].cellType = CellType.Shop;
                                gridArray[x, y].spriteRenderer.sprite = shopCell;
                                gridArray[x, y].spriteRenderer.color = Color.cyan;
                                break;
                            case CellType.Empty:
                            default:
                                gridArray[x, y].cellType = CellType.Empty;
                                gridArray[x, y].spriteRenderer.sprite = emptyCell;
                                gridArray[x, y].spriteRenderer.color = Color.white;
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
                    spawnedTile.transform.parent = _parent;
                    spawnedTile.cellType = CellType.Empty;
                    gridArray[x, y] = spawnedTile;
                    gridArray[x, y].spriteRenderer.color = Color.white;
                }
            }

            _parent.localScale = new Vector3(0.245f, 0.245f, 1);
            _parent.localPosition = new Vector3(-0.464f, 1.18f, 10.046f);
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
                case CellType.PlayerOnShop:
                    gridArray[_playerX, _playerY].cellType = CellType.Shop;
                    gridArray[_playerX, _playerY].spriteRenderer.sprite = shopCell;
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
                case CellType.Shop:
                    gridArray[_playerX, _playerY].cellType = CellType.PlayerOnShop;
                    gridArray[_playerX, _playerY].spriteRenderer.sprite = playerOnShopCell;
                    break;
            }
        }

        private void HandleDockCommand()
        {
            if (isDocked)
            {
                EventManager.InvokeDockWhenDocked();
            }

            if (gridArray[_playerX, _playerY].cellType != CellType.PlayerOnShop)
            {
                EventManager.InvokeDockedFalse();
            }

            if (gridArray[_playerX, _playerY].cellType == CellType.PlayerOnShop)
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
            else
            {
                EventManager.InvokeUndockedFalse();
            }
        }

        private void HandleCollectCommand()
        {
            if (gridArray[_playerX, _playerY].cellType == CellType.PlayerOnLoot)
            {
                EventManager.InvokeCollectedItemTrue();
                gridArray[_playerX, _playerY].cellType = CellType.Player;
                gridArray[_playerX, _playerY].spriteRenderer.sprite = playerCell;
            }
            else
            {
                EventManager.InvokeCollectedItemFalse();
            }
        }

        #endregion
    }
}