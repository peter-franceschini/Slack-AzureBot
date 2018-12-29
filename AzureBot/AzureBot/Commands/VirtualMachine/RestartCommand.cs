using AzureBot.Services;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class RestartCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }
        private ICommandParseService CommandParseService { get; set; }

        public RestartCommand()
        {
            VirtualMachineService = new VirtualMachineService();
            CommandParseService = new VirtualMachineCommandParseService();
        }

        public bool CanExecute(string commandText)
        {
            var command = CommandParseService.ParseCommand(commandText);
            if (command.Action == "restart")
            {
                return true;
            }

            return false;
        }

        public void Execute(string machineName)
        {
            if (VirtualMachineService.IsRunning(machineName))
            {
                VirtualMachineService.Restart(machineName);
            }
        }
    }
}
