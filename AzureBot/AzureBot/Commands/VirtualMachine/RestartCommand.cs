﻿using AzureBot.Models;
using AzureBot.Services.AzureServices;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class RestartCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }
        private VirtualMachineCommand Command { get; set; }
        private bool Success { get; set; }

        public RestartCommand(IVirtualMachineService virtualMachineService, VirtualMachineCommand command)
        {
            VirtualMachineService = virtualMachineService;
            Command = command;
        }

        public static bool CanExecute(string action)
        {
            if (action == "restart")
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            if (VirtualMachineService.IsRunning(Command.Target))
            {
                VirtualMachineService.Restart(Command.Target);
            }
        }

        public string GetResultMessage()
        {
            if (Success)
            {
                return $"{Command.Target} restarted";
            }
            else
            {
                return $"Unable to restart {Command.Target}";
            }
        }
    }
}
