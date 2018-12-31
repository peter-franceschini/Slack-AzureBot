using AzureBot.Models;
using AzureBot.Services.AzureServices;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class StartCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }
        private VirtualMachineCommand Command { get; set; }
        private bool Success { get; set; }

        public StartCommand(IVirtualMachineService virtualMachineService, VirtualMachineCommand command)
        {
            VirtualMachineService = virtualMachineService;
            Command = command;
        }

        public static bool CanExecute(string action)
        {
            if(action == "start" 
                || action == "start up" 
                || action == "turn on" 
                || action == "power on" 
                || action == "power up")
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            // If the virtual machine is off
            if (!VirtualMachineService.IsRunning(Command.Target))
            {
                VirtualMachineService.Start(Command.Target);
                Success = true;
            }
        }

        public string GetResultMessage()
        {        
            if (Success)
            {
                return $"{Command.Target} started";
            }
            else
            {
                return $"Unable to start {Command.Target}";
            }
        }
    }
}
