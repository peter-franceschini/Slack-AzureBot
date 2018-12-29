using AzureBot.Services;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class StartCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }
        private ICommandParseService CommandParseService { get; set; }

        public StartCommand()
        {
            VirtualMachineService = new VirtualMachineService();
            CommandParseService = new VirtualMachineCommandParseService();
        }

        public bool CanExecute(string commandText)
        {
            var command = CommandParseService.ParseCommand(commandText);
            if(command.Action == "start" 
                || command.Action == "start up" 
                || command.Action == "turn on" 
                || command.Action == "power on" 
                || command.Action == "power up")
            {
                return true;
            }

            return false;
        }

        public void Execute(string machineName)
        {
            if (!VirtualMachineService.IsRunning(machineName))
            {
                VirtualMachineService.Start(machineName);
            }
        }
    }
}
