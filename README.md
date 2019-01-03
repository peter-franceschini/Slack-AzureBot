# AzureBot - Slack Bot for Azure
AzureBot is an ASP.NET Core 2.2 implementation of the Slack Slash Command API, paired with Azure's Management Libraries service to allow Slack users to control Azure resources (at this time virtual machines) from Slack Slash Commands.

## Commands
* */Azurebot help* - Displays help message
* */Azurebot start [virtual machine name]* - Starts the virtual machine
* */Azurebot stop [virtual machine name]* - Stops the virtual machine
* */Azurebot restart [virtual machine name]* - Restarts the virtual machine
* */Azurebot powerstate [virtual machine name]* - Retrieves the virtual machine's power state 

## Upcoming Features
* Alias feature to allow control of multiple virtual machines with a single command
* Commands for additional Azure resources

## Integrations
* [Slack Slash Commands](https://api.slack.com/slash-commands)
* [Microsoft Azure Management Libraries for .NET (Fluent API)](https://github.com/Azure/azure-libraries-for-net)

## Installation / Usage

### Requirements
* .NET Core 2.2
* Slack Team with administrative privileges
* Azure Subscription with administrative privileges

### Application Setup
1. Pull the latest code from the master branch of this repository
1. Build and publish the application to your desired web server
    * Web server must have [.Net Core 2.2 Runtime](https://dotnet.microsoft.com/download/dotnet-core/2.2) installed 

### Slack Setup
1. Setup a new Slack App in the [Slack API dashboard](https://api.slack.com/apps)
1. Update your application configuration file or secrets storage with your Slack Singing Secret from the App Credentials section of your new Slack App
1. Add the Slash Command feature to your new Slack App
1. Add a new Command with the following details:
    * Command: /Azurebot
    * Request URL: [Your Server URL]/api/SlashCommand
    * Short Description: [Azure command]
    * Usage Hint: [Azure command]
1. Deploy your new Slack app to your Slack Team

### Azure Setup
1. Obtain Azure credentials for your subscription and a service principal
    * [How to: Use the portal to create an Azure AD application and service principal that can access resources](https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal)
    * [Give applications access to Azure Stack resources by creating service principals](https://docs.microsoft.com/en-us/azure/azure-stack/user/azure-stack-create-service-principals#assign-role-to-service-principal)
1. Update your application configuration file or secrets storage with your Azure Client Id, Client Secret and Tenant Id obtained in step 1

### Testing
1. Open Slack and invoke */Azurebot help*
1. Success!