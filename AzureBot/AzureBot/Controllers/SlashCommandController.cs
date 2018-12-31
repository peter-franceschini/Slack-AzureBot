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
        private ICommandExecutionService CommandExecutionService { get; set; }

        public SlashCommandController(IOptions<SlackSettings> slackSettings, ISignatureValidationService signatureValidationService, ICommandExecutionService commandExecutionService)
        {
            SlackSettings = slackSettings.Value;
            SignatureValidationService = signatureValidationService;
            CommandExecutionService = commandExecutionService;
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

            // Validate command and return error to Slack if the command is invalid
            if (!CommandExecutionService.IsCommandValid(slashCommandPayload.Text))
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

            // TODO: Make Async!
            CommandExecutionService.Execute(slashCommandPayload);

            // Send response to Slack
            var slashCommandResponse = new SlashCommandResponse()
            {
                ResponseType = "in_channel",
                Text = "Executing Command"
            };
            
            return Ok(slashCommandResponse);
        }
        
        [HttpGet]
        [Produces("application/json")]
        // Used for testing
        public async Task<IActionResult> Get()
        {
            var payload = new SlashCommandPayload()
            {
                Text = "start Demo-Server",
            };

            if (CommandExecutionService.IsCommandValid(payload.Text))
            {
                CommandExecutionService.Execute(payload);
            }

            return Ok();
        }
    }
}
