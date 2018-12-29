using AzureBot.Factories;
using AzureBot.Models.Slack;
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

        public SlashCommandController(IOptions<SlackSettings> slackSettings, ISignatureValidationService signatureValidationService, IVirtualMachineCommandFactory virtualMachineCommandFactory)
        {
            SlackSettings = slackSettings.Value;
            SignatureValidationService = signatureValidationService;
            VirtualMachineCommandFactory = virtualMachineCommandFactory;
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
            var command = VirtualMachineCommandFactory.GetCommand(slashCommandPayload.Text);
            if(command == null)
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

            command.Execute(slashCommandPayload.Text);

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
        public async Task<IActionResult> Get(string commandText)
        {
            var factory = new VirtualMachineCommandFactory();
            var command = factory.GetCommand(commandText);
            command.Execute(commandText);

            return Ok();
        }
    }
}
