using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Models
{
    public class VirtualMachineCommand
    {
        public string Action { get; set; }
        public string Target { get; set; }
    }
}
