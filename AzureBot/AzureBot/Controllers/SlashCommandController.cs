using AzureBot.Factories;
using AzureBot.Models.Slack;
using AzureBot.Services.AzureServices;
using AzureBot.Services.Slack;
using AzureBot.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AzureBot.Controllers
{
    [Route("api/[controller]")]
    public class SlashCommandController : Controller
    {
        private SlackSettings SlackSettings { get; set; }
        private ISignatureValidationService SignatureValidationService { get; set; }
        private IVirtualMachineCommandFactory VirtualMachineCommandFactory { get; set; }
        private ICommandParseService CommandParseService { get; set; }

        public SlashCommandController(IOptions<SlackSettings> slackSettings, ISignatureValidationService signatureValidationService, IVirtualMachineCommandFactory virtualMachineCommandFactory, ICommandParseService commandParseService)
        {
            SlackSettings = slackSettings.Value;
            SignatureValidationService = signatureValidationService;
            VirtualMachineCommandFactory = virtualMachineCommandFactory;
            CommandParseService = commandParseService;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post()
        {
            var streamHelper = new StreamHelper();
            var requestBody = streamHelper.ReadAsString(Request.Body).Result;

            var slashCommandPayload = new SlashCommandPayload(requestBody);

            // Verify request signature
            if (!SignatureValidationService.SignatureValid(Request.Headers["X-Slack-Signature"], Request.Headers["X-Slack-Request-Timestamp"], requestBody, SlackSettings.SignatureSecret))
            {
                return BadRequest();
            }

            // TODO: Move action execution to Async implementation, and send postback to Slack using response url once complete with full status

            // Execute command action
            var slackCommand = CommandParseService.ParseCommand(slashCommandPayload.Text);
            var command = VirtualMachineCommandFactory.GetCommand(slackCommand.Action);

            if (command == null)
            {
                // No command was found to execute this request, send error response to Slack
                var slashCommandErrorResponse = new SlashCommandResponse()
                {
                    ResponseType = "ephemeral",
                    Text = "Sorry, we were unable to understand your request. Please try again or type /help for more information."
                };

                // Even though an error occured, Slack needs a 200 OK response code (see Slack docs)
                return Ok(slashCommandErrorResponse);
            }

            command.Execute(slackCommand.Target);

            // Send response to Slack
            var slashCommandResponse = new SlashCommandResponse()
            {
                ResponseType = "ephemeral",
                Text = "Command Executed"
            };
            
            return Ok(slashCommandResponse);
        }
        
        [HttpGet]
        [Produces("application/json")]
        // Used for testing
        public async Task<IActionResult> Get(string slackText)
        {
            var slackCommand = CommandParseService.ParseCommand(slackText);
            var command = VirtualMachineCommandFactory.GetCommand(slackCommand.Action);
            command.Execute(slackCommand.Target);

            return Ok();
        }
    }
}
