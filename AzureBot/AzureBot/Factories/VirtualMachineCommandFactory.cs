using AzureBot.Commands.VirtualMachine;

namespace AzureBot.Factories
{
    public class VirtualMachineCommandFactory : IVirtualMachineCommandFactory
    {
        public IVirtualMachineCommand GetCommand(string command)
        {
            if (RestartCommand.CanExecute(command))
            {
                return new RestartCommand();
            }
            else if (StartCommand.CanExecute(command))
            {
                return new StartCommand();
            }
            else if (StopCommand.CanExecute(command))
            {
                return new StopCommand();
            }

            return null;
        }
    }
}
