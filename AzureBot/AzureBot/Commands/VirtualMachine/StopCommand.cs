using AzureBot.Services;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class StopCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }

        public StopCommand(IVirtualMachineService virtualMachineService)
        {
            VirtualMachineService = virtualMachineService;
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

        public void Execute(string target)
        {
            if (VirtualMachineService.IsRunning(target))
            {
                VirtualMachineService.Stop(target);
            }
        }
    }
}
