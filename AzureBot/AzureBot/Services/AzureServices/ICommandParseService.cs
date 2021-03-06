﻿using AzureBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Services.AzureServices
{
    public interface ICommandParseService
    {
        VirtualMachineCommand ParseCommand(string text);
    }
}
