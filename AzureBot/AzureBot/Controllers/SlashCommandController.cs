using AzureBot.Models.Slack;
using AzureBot.Services.Slack;
using AzureBot.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Controllers
{
    [Route("api/[controller]")]
    public class SlashCommandController : Controller
    {
        private SlackSettings SlackSettings { get; set; }
        private ISignatureValidationService SignatureValidationService { get; set; }

        public SlashCommandController(IOptions<SlackSettings> slackSettings, ISignatureValidationService signatureValidationService)
        {
            SlackSettings = slackSettings.Value;
            SignatureValidationService = signatureValidationService;
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

            // TODO - do stuff here with the slash command retrieved

            // Send response to Slack
            SlashCommandResponse slashCommandResponse = new SlashCommandResponse();
            
            return Ok(slashCommandResponse);
        }
    }
}
