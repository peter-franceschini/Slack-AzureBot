using AzureBot.Commands.VirtualMachine;
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

        public IVirtualMachineCommand GetCommand(string command)
        {
            if (RestartCommand.CanExecute(command))
            {
                return new RestartCommand(VirtualMachineService);
            }
            else if (StartCommand.CanExecute(command))
            {
                return new StartCommand(VirtualMachineService);
            }
            else if (StopCommand.CanExecute(command))
            {
                return new StopCommand(VirtualMachineService);
            }

            return null;
        }
    }
}
