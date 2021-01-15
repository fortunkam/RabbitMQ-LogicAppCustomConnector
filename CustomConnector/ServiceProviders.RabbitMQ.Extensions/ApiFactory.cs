using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using System;

namespace ServiceProviders.RabbitMQ.Extensions
{
    public static class ApiFactory
    {

        /// The service name.
        /// </summary>
        public const string ServiceName = "rabbitmq";

        /// <summary>
        /// The service id.
        /// </summary>
        public const string ServiceId = "/serviceProviders/rabbitmq";

        public static Uri IconUri = new Uri("https://raw.githubusercontent.com/fortunkam/RabbitMQ-LogicAppCustomConnector/main/CustomConnector/ServiceProviders.RabbitMQ.Extensions/icon.png");

        public const uint BrandColor = 0x188E3A;

        /// <summary>
        /// The Azure cosmos db API.
        /// </summary>
        public static ServiceOperationApi OperationApi = new ServiceOperationApi
        {
            Name = ApiFactory.ServiceName,
            Id = ApiFactory.ServiceId,
            Type = DesignerApiType.ServiceProvider,
            Properties = new ServiceOperationApiProperties
            {
                BrandColor = ApiFactory.BrandColor,
                Description = "Connect to Azure Cosmos db to receive document.",
                DisplayName = "Memoryleek RabbitMQ",
                IconUri = ApiFactory.IconUri,
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
