﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBot.Services.Slack
{
    public interface ISignatureValidationService
    {
        bool SignatureValid(string xSlackSignature, string xSlackRequestTimestamp, string requestBody, string signatureSecret);
    }
}
