﻿using AzureBot.Services;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class StopCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }
        private ICommandParseService CommandParseService { get; set; }

        public StopCommand()
        {
            VirtualMachineService = new VirtualMachineService();
            CommandParseService = new VirtualMachineCommandParseService();
        }

        public bool CanExecute(string commandText)
        {
            var command = CommandParseService.ParseCommand(commandText);
            if (command.Action == "stop"
                || command.Action == "power off"
                || command.Action == "shut down"
                || command.Action == "turn off")
            {
                return true;
            }

            return false;
        }

        public void Execute(string machineName)
        {
            if (VirtualMachineService.IsRunning(machineName))
            {
                VirtualMachineService.Stop(machineName);
            }
        }
    }
}
