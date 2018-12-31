using AzureBot.Commands.VirtualMachine;
using AzureBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Factories
{
    public interface IVirtualMachineCommandFactory
    {
        IVirtualMachineCommand GetCommand(VirtualMachineCommand command);
    }
}
