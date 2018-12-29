using AzureBot.Commands.VirtualMachine;

namespace AzureBot.Factories
{
    public class VirtualMachineCommandFactory : IVirtualMachineCommandFactory
    {
        public IVirtualMachineCommand GetCommand(string slackText)
        {
            if (new RestartCommand().CanExecute(slackText))
            {
                return new RestartCommand();
            }
            else if (new StartCommand().CanExecute(slackText))
            {
                return new StartCommand();
            }
            else if (new StopCommand().CanExecute(slackText))
            {
                return new StopCommand();
            }

            return null;
        }
    }
}
