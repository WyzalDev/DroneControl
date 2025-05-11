using System;
using _DroneControl.Audio;
using UnityEngine;

namespace _DroneControl.TerminalPanel.Console
{
    public class Validator : MonoBehaviour
    {
        public void ValidateAndSendCommand(string command)
        {
            AudioStorage.PlayGlobalSfx("submitCommand");
            if (command.Length < 4)
            {
                CommandHandler.UnknowCommand();
                return;
            }

            command = command.ToLower();
            Debug.Log(command);

            switch (command[0])
            {
                case 'm': //move Up,Down,Left,Right
                    if (command.Length >= 7)
                    {
                        ValidateMoveCommands(command);
                    }
                    else
                    {
                        CommandHandler.UnknowCommand();
                    }

                    return;
                case 'd': //dock or delete
                    ValidateDockOrDeleteCommand(command);
                    return;
                case 'u': //undock
                    if (command.Length >= 6 && command[1] == 'n' && command[2] == 'd' && command[3] == 'o' &&
                        command[4] == 'c' && command[5] == 'k' && OtherSymbolsSpacesOrNothing(command, 6))
                        CommandHandler.AddShipCommandToQueue(ShipCommand.UnDock);
                    else
                        CommandHandler.UnknowCommand();
                    return;
                case 'c': //collect
                    if (command.Length >= 7 && command[1] == 'o' && command[2] == 'l' && command[3] == 'l' &&
                        command[4] == 'e' && command[5] == 'c' && command[6] == 't' &&
                        OtherSymbolsSpacesOrNothing(command, 7))
                        CommandHandler.AddShipCommandToQueue(ShipCommand.Collect);
                    else
                        CommandHandler.UnknowCommand();
                    return;
                case 'l': //list
                    if (command.Length >= 4 && command[1] == 'i' && command[2] == 's' && command[3] == 't' &&
                        OtherSymbolsSpacesOrNothing(command, 4))
                        CommandHandler.HandleListCommand();
                    else
                        CommandHandler.UnknowCommand();
                    return;
                case 's': //start
                    if (command.Length >= 5 && command[1] == 't' && command[2] == 'a' && command[3] == 'r' &&
                        command[4] == 't' && OtherSymbolsSpacesOrNothing(command, 5))
                        CommandHandler.HandleStartCommand();
                    else
                        CommandHandler.UnknowCommand();
                    return;
                default:
                    CommandHandler.UnknowCommand();
                    return;
            }
        }

        private void ValidateMoveCommands(string command)
        {
            if (command[1] == 'o' && command[2] == 'v' && command[3] == 'e' && command[4] == ' ')
            {
                ValidateMoveSecondCommand(command);
            }
            else
            {
                CommandHandler.UnknowCommand();
            }
        }

        private void ValidateMoveSecondCommand(string command)
        {
            switch (command[5])
            {
                case 'u':
                    if (command[6] == 'p' && OtherSymbolsSpacesOrNothing(command, 7))
                        CommandHandler.AddShipCommandToQueue(ShipCommand.MoveUp);
                    else
                        CommandHandler.UnknowCommand();
                    return;
                case 'd':
                    if (command.Length >= 9 && command[6] == 'o' && command[7] == 'w' && command[8] == 'n' &&
                        OtherSymbolsSpacesOrNothing(command, 9))
                        CommandHandler.AddShipCommandToQueue(ShipCommand.MoveDown);
                    else
                        CommandHandler.UnknowCommand();
                    return;
                case 'r':
                    if (command.Length >= 10 && command[6] == 'i' && command[7] == 'g' && command[8] == 'h' &&
                        command[9] == 't' &&
                        OtherSymbolsSpacesOrNothing(command, 10))
                        CommandHandler.AddShipCommandToQueue(ShipCommand.MoveRight);
                    else
                        CommandHandler.UnknowCommand();
                    return;
                case 'l':
                    if (command.Length >= 9 && command[6] == 'e' && command[7] == 'f' && command[8] == 't' &&
                        OtherSymbolsSpacesOrNothing(command, 9))
                        CommandHandler.AddShipCommandToQueue(ShipCommand.MoveLeft);
                    else
                        CommandHandler.UnknowCommand();
                    return;
                default:
                    CommandHandler.UnknowCommand();
                    return;
            }
        }

        private void ValidateDockOrDeleteCommand(string command)
        {
            switch (command[1])
            {
                case 'e':
                    if (command.Length >= 8 && command[2] == 'l' && command[3] == 'e' && command[4] == 't' &&
                        command[5] == 'e' && command[6] == ' ' && Char.IsNumber(command, 7) &&
                        OtherSymbolsSpacesOrNothing(command, 8))
                        CommandHandler.HandleDeleteCommand((command[7]) - '0');
                    else
                        CommandHandler.UnknowCommand();
                    return;
                case 'o':
                    if (command[2] == 'c' && command[3] == 'k' && OtherSymbolsSpacesOrNothing(command, 4))
                        CommandHandler.AddShipCommandToQueue(ShipCommand.Dock);
                    else
                        CommandHandler.UnknowCommand();
                    return;
                default:
                    CommandHandler.UnknowCommand();
                    return;
            }
        }

        private bool OtherSymbolsSpacesOrNothing(string command, int index)
        {
            for (int i = index; i < command.Length; i++)
            {
                if (command[i] != ' ') return false;
            }

            return true;
        }
    }
}