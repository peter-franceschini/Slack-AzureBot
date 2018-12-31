using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureBot.Commands.VirtualMachine;
using AzureBot.Factories;
using AzureBot.Models.Slack;

namespace AzureBot.Services.AzureServices
{
    public class CommandExecutionService : ICommandExecutionService
    {
        private IVirtualMachineCommandFactory VirtualMachineCommandFactory { get; set; }
        private ICommandParseService CommandParseService { get; set; }

        public CommandExecutionService(IVirtualMachineCommandFactory virtualMachineCommandFactory, ICommandParseService commandParseService)
        {
            VirtualMachineCommandFactory = virtualMachineCommandFactory;
            CommandParseService = commandParseService;
        }

        public bool IsCommandValid(string commandText)
        {
            var slackCommand = CommandParseService.ParseCommand(commandText);
            var command = VirtualMachineCommandFactory.GetCommand(slackCommand);

            return command != null;
        }

        public void Execute(SlashCommandPayload slashCommandPayload)
        {
            // Parse the command
            var slackCommand = CommandParseService.ParseCommand(slashCommandPayload.Text);
            // Find the right command to execute
            var command = VirtualMachineCommandFactory.GetCommand(slackCommand);

            // Execute command
            command.Execute();

            // Send response to slack


        }
    }
}
