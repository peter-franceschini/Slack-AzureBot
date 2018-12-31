using AzureBot.Models.AzureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Commands.VirtualMachine
{
    public interface IVirtualMachineCommand
    {
        void Execute();
        string GetResultMessage();
    }
}
