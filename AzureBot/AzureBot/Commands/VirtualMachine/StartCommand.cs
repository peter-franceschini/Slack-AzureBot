﻿using AzureBot.Services.AzureServices;
using System;

namespace AzureBot.Commands.VirtualMachine
{
    public class StartCommand : IVirtualMachineCommand
    {
        private IVirtualMachineService VirtualMachineService { get; set; }

        public StartCommand(IVirtualMachineService virtualMachineService)
        {
            VirtualMachineService = virtualMachineService;
        }

        public static bool CanExecute(string action)
        {
            if(action == "start" 
                || action == "start up" 
                || action == "turn on" 
                || action == "power on" 
                || action == "power up")
            {
                return true;
            }

            return false;
        }

        public void Execute(string target)
        {
            if (!VirtualMachineService.IsRunning(target))
            {
                VirtualMachineService.Start(target);
            }
        }
    }
}
