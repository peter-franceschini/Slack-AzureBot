using AzureBot.Commands.VirtualMachine;
using AzureBot.Models;
using AzureBot.Services.AzureServices;

namespace AzureBot.Factories
{
    public class VirtualMachineCommandFactory : IVirtualMachineCommandFactory
    {
        private IVirtualMachineService VirtualMachineService { get; set; }

        public VirtualMachineCommandFactory(IVirtualMachineService virtualMachineService)
        {
            VirtualMachineService = virtualMachineService;
        }

        public IVirtualMachineCommand GetCommand(VirtualMachineCommand command)
        {
            if (RestartCommand.CanExecute(command.Action))
            {
                return new RestartCommand(VirtualMachineService, command);
            }
            else if (StartCommand.CanExecute(command.Action))
            {
                return new StartCommand(VirtualMachineService, command);
            }
            else if (StopCommand.CanExecute(command.Action))
            {
                return new StopCommand(VirtualMachineService, command);
            }

            return null;
        }
    }
}
