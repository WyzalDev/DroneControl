using System.Collections.Generic;
using _DroneControl.Audio;
using _DroneControl.Scripts;
using _DroneControl.Scripts.Battery;
using _DroneControl.TerminalPanel.Minigame;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Console
{
    public class CommandHandler : MonoBehaviour
    {
        [SerializeField] private InfoPanel _infoPanel;
        [SerializeField] private BatteryStorage batteryStorage;
        [Range(3,8)]
        [SerializeField] public int maxCommands = 5;

        public static CommandHandler Instance;

        public List<ShipCommand> CommandQueue = new List<ShipCommand>();

        private const string COMMAND_ADDED = "\"{0}\" added to queue";
        private const string COMMAND_CANT_BE_ADDED = "<color=\"red\">Queue is overflowing</color>";
        private const string UNKNOWN_COMMAND = "<color=\"red\">Unknown command</color>";
        private const string LIST_COMMAND = "Commands in queue :";
        private const string LIST_ELEMENT = "{0}. {1}";
        private const string DELETE_ELEMENT = "Delete command on {0} position";
        private const string DELETE_ELEMENT_ERROR_TOO_HIGH = "<color=\"red\">Delete range error</color>";
        private const string START_ERROR_NO_COMMANDS = "<color=\"red\">No commands in queue</color>";
        private const string START_COMMAND_HANDLING = "Start command handling";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void AddShipCommandToQueue(ShipCommand command)
        {
            Debug.Log(command);
            if (Instance.CommandQueue.Count < Instance.maxCommands)
            {
                Instance.CommandQueue.Add(command);
                Instance._infoPanel.AddLine(string.Format(COMMAND_ADDED, command.ToString()));
            }
            else
            {
                Instance._infoPanel.AddLine(COMMAND_CANT_BE_ADDED);
                AudioStorage.PlayGlobalSfx("cantDoThat");
            }
        }

        public static void HandleStartCommand()
        {
            if (Instance.CommandQueue.Count == 0)
            {
                Instance._infoPanel.AddLine(START_ERROR_NO_COMMANDS);
                AudioStorage.PlayGlobalSfx("cantDoThat");
                return;
            }
            
            Instance.batteryStorage.UseOneBattery();
            EventManager.InvokeOneBatteryUsed();
            
            Instance._infoPanel.AddLine(START_COMMAND_HANDLING);
            GridManager.HandleAllShipCommands(Instance.CommandQueue);
            Instance.CommandQueue.Clear();
            
            if (BatteryStorage.currentAmount <= 0 && !GridManager.isDocked)
            {
                EventManager.InvokeEndGame(false);
            }
        }
        
        public static void HandleListCommand()
        {
            Instance._infoPanel.AddLine(LIST_COMMAND);
            for (var i = 0; i < Instance.CommandQueue.Count; i++)
            {
                Instance._infoPanel.AddLine(string.Format(LIST_ELEMENT, i, Instance.CommandQueue[i].ToString()));
            }
        }
        
        public static void HandleDeleteCommand(int number)
        {
            if (Instance.CommandQueue.Count > number)
            {
                Instance.CommandQueue.RemoveAt(number);
                Instance._infoPanel.AddLine(string.Format(DELETE_ELEMENT, number));
            }
            else
            {
                Instance._infoPanel.AddLine(DELETE_ELEMENT_ERROR_TOO_HIGH);
            }
        }

        public static void UnknowCommand()
        {
            Instance._infoPanel.AddLine(UNKNOWN_COMMAND);
            AudioStorage.PlayGlobalSfx("cantDoThat");
        }
    }

    public enum ShipCommand
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Dock,
        UnDock,
        Collect
    }
}