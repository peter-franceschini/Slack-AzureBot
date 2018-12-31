using AzureBot.Models.AzureModels;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Extensions.Options;
using System.Linq;

namespace AzureBot.Services
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private IAzure _Azure { get; set; }
        private AzureSettings AzureSettings { get; set; }

        public VirtualMachineService(IOptions<AzureSettings> azureSettings)
        {
            AzureSettings = azureSettings.Value;

            var credentials = new AzureCredentialsFactory().FromServicePrincipal(
                AzureSettings.ClientId,
                AzureSettings.ClientSecret,
                AzureSettings.TenantId, 
                AzureEnvironment.AzureGlobalCloud);
            _Azure = Azure.Authenticate(credentials).WithDefaultSubscription();
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
