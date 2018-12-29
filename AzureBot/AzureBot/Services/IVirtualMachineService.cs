namespace AzureBot.Services
{
    public interface IVirtualMachineService
    {
        void Start(string machineName);
        void Stop(string machineName);
        void Restart(string machineName);
        bool IsRunning(string machineName);
    }
}
