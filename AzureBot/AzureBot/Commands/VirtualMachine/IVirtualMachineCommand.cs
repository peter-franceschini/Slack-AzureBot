using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Commands.VirtualMachine
{
    public interface IVirtualMachineCommand
    {
        bool CanExecute(string commandText);
        void Execute(string machineName);
    }
}
