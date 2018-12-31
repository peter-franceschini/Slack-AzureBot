using AzureBot.Models;
using AzureBot.Services.AzureServices;
using Microsoft.Azure.Management.Compute.Fluent;

namespace AzureBot.Commands.VirtualMachine
{
    public class PowerStateCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }
        private VirtualMachineCommand Command { get; set; }
        private PowerState PowerState { get; set; }

        public PowerStateCommand(IVirtualMachineService virtualMachineService, VirtualMachineCommand command)
        {
            VirtualMachineService = virtualMachineService;
            Command = command;
        }

        public static bool CanExecute(string action)
        {
            if (action == "powerstate")
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            var powerState = VirtualMachineService.GetPowerState(Command.Target);
        }

        public string GetResultMessage()
        {
            return $"{Command.Target} is in power state {PowerState.ToString()}";
        }
    }
}
