using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using System;

namespace ServiceProviders.RabbitMQ.Extensions
{
    public static class ApiFactory
    {

        /// <summary>
        /// The Azure cosmos db API.
        /// </summary>
        public static ServiceOperationApi OperationApi = new ServiceOperationApi
        {
            Name = "memleek_rabbitmq",
            Id = "/serviceProviders/memleek_rabbitmq",
            Type = DesignerApiType.ServiceProvider,
            Properties = new ServiceOperationApiProperties
            {
                BrandColor = 0x188E3A,
                Description = "Connect to Azure Cosmos db to receive document.",
                DisplayName = "Memoryleek RabbitMQ",
                IconUri = new Uri("https://raw.githubusercontent.com/fortunkam/RabbitMQ-LogicAppCustomConnector/main/CustomConnector/ServiceProviders.RabbitMQ.Extensions/icon.png"),
                Capabilities = new ApiCapability[] { ApiCapability.Triggers, ApiCapability.Actions },
                ConnectionParameters = new ConnectionParameters
                {
                    ConnectionString = new ConnectionStringParameters
                    {
                        Type = ConnectionStringType.SecureString,
                        ParameterSource = ConnectionParameterSource.AppConfiguration,
                        UIDefinition = new UIDefinition
                        {
                            DisplayName = "RabbitMQ Connection String",
                            Description = "RabbitMQ Connection String",
                            Tooltip = "Provide RabbitMQ Connection String",
                            Constraints = new Constraints
                            {
                                Required = "true",
                            },
                        },
                    },
                },
            },
        };
    }
}
