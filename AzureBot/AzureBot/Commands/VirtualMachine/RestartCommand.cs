using AzureBot.Services;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class RestartCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }

        public RestartCommand()
        {
            VirtualMachineService = new VirtualMachineService();
        }

        public static bool CanExecute(string action)
        {
            if (action == "restart")
            {
                return true;
            }

            return false;
        }

        public void Execute(string target)
        {
            if (VirtualMachineService.IsRunning(target))
            {
                VirtualMachineService.Restart(target);
            }
        }
    }
}
