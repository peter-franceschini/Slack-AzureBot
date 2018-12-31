using AzureBot.Services;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class RestartCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }

        public RestartCommand(IVirtualMachineService virtualMachineService)
        {
            VirtualMachineService = virtualMachineService;
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
