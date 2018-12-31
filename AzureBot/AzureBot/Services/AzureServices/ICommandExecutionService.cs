using AzureBot.Models.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Services.AzureServices
{
    public interface ICommandExecutionService
    {
        bool IsCommandValid(string commandText);
        void Execute(SlashCommandPayload slashCommandPayload);
    }
}
