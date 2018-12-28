using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Services
{
    public interface IVirtualMachineService
    {
        void Start(string machineName);
        void Stop(string machineName);
        bool IsRunning(string machineName);
    }
}
