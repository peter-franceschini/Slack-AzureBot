using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureBot.Factories;
using AzureBot.Models.Slack;
using AzureBot.Services.Slack;

namespace AzureBot.Services.AzureServices
{
    public class CommandExecutionService : ICommandExecutionService
    {
        private IVirtualMachineCommandFactory VirtualMachineCommandFactory { get; set; }
        private ICommandParseService CommandParseService { get; set; }
        private ISlackDelayedResponseService SlackDelayedResponseService { get; set; }

        public CommandExecutionService(IVirtualMachineCommandFactory virtualMachineCommandFactory, ICommandParseService commandParseService, ISlackDelayedResponseService slackDelayedResponseService)
        {
            VirtualMachineCommandFactory = virtualMachineCommandFactory;
            CommandParseService = commandParseService;
            SlackDelayedResponseService = slackDelayedResponseService;
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
            var responseModel = new SlashCommandResponse()
            {
                ResponseType = "in_channel",
                Text = command.GetResultMessage()
            };
            SlackDelayedResponseService.SendDelayedResponse(slashCommandPayload.ResponseUrl, responseModel);
        }
    }
}
