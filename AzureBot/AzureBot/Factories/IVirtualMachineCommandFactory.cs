using AzureBot.Commands.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Factories
{
    public interface IVirtualMachineCommandFactory
    {
        IVirtualMachineCommand GetCommand(string slackText);
    }
}
