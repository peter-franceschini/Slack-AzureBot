using AzureBot.Models;

namespace AzureBot.Services
{
    public class VirtualMachineCommandParseService : ICommandParseService
    {
        public VirtualMachineCommand ParseCommand(string text)
        {
            text = text.ToLower();
            var parts = text.Split(" ");

            if (parts.Length == 2)
            {
                return new VirtualMachineCommand()
                {
                    Action = parts[0],
                    Target = parts[1]
                };
            }
            else if (parts.Length == 3)
            {
                return new VirtualMachineCommand()
                {
                    Action = $"{parts[0]} {parts[1]}",
                    Target = parts[2]
                };
            }

            return new VirtualMachineCommand();
        }
    }
}
