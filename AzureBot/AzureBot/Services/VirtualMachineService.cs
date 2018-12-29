using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Fluent;
using System.Linq;

namespace AzureBot.Services
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private IAzure _Azure { get; set; }

        public VirtualMachineService()
        {
            _Azure = Azure.Authenticate("Cred_file_path").WithDefaultSubscription();
        }

        public void Start(string machineName)
        {
            var vm = GetVirtualMachineByName(machineName);
            vm.Start();
        }

        public void Stop(string machineName)
        {
            var vm = GetVirtualMachineByName(machineName);
            vm.PowerOff();
        }

        public bool IsRunning(string machineName)
        {
            var machine = GetVirtualMachineByName(machineName);
            return machine.PowerState == PowerState.Running;
        }

        private IVirtualMachine GetVirtualMachineByName(string machineName)
        {
            var virtualMachines = _Azure.VirtualMachines.List();
            return virtualMachines.FirstOrDefault(v => v.Name.ToLower() == machineName.ToLower());
        }

        public void Restart(string machineName)
        {
            var vm = GetVirtualMachineByName(machineName);
            vm.Restart();
        }
    }
}
