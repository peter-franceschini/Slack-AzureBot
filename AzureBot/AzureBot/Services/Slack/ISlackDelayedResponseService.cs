using AzureBot.Models.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Services.Slack
{
    public interface ISlackDelayedResponseService
    {
        void SendDelayedResponse(string responseUrl, SlashCommandResponse response);
    }
}
