using AzureBot.Models;
using AzureBot.Services.AzureServices;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class StopCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }
        private VirtualMachineCommand Command { get; set; }
        private bool Success { get; set; }

        public StopCommand(IVirtualMachineService virtualMachineService, VirtualMachineCommand command)
        {
            VirtualMachineService = virtualMachineService;
            Command = command;
        }

        public static bool CanExecute(string action)
        {
            if (action == "stop"
                || action == "power off"
                || action == "shut down"
                || action == "turn off")
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            if (VirtualMachineService.IsRunning(Command.Target))
            {
                VirtualMachineService.Stop(Command.Target);
            }
        }

        public string GetResultMessage()
        {
            if (Success)
            {
                return $"{Command.Target} stopped";
            }
            else
            {
                return $"Unable to stop {Command.Target}";
            }
        }
    }
}
