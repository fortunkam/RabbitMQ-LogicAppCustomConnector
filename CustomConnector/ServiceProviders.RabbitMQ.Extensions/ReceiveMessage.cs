using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Swagger.Entities;
using Newtonsoft.Json.Linq;
using System;

namespace ServiceProviders.RabbitMQ.Extensions
{
    public static class ReceiveMessage
    {
        public static readonly ServiceOperationManifest Manifiest = new ServiceOperationManifest
        {
            ConnectionReference = new ConnectionReferenceFormat
            {
                ReferenceKeyFormat = ConnectionReferenceKeyFormat.ServiceProvider,
            },
            Settings = new OperationManifestSettings
            {
                SecureData = new OperationManifestSettingWithOptions<SecureDataOptions>(),
                TrackedProperties = new OperationManifestSetting
                {
                    Scopes = new OperationScope[] { OperationScope.Trigger },
                },
            },
            InputsLocation = new InputsLocation[]
            {
                InputsLocation.Inputs,
                InputsLocation.Parameters,
            },
            Outputs = new SwaggerSchema
            {
                Type = SwaggerSchemaType.Object,
                Properties = new OrdinalDictionary<SwaggerSchema>
                {
                    {
                        "body", new SwaggerSchema
                        {
                            Type = SwaggerSchemaType.Object,

                            Title = "Receive message",
                            Description = "Receive message",
                            Properties = new OrdinalDictionary<SwaggerSchema>
                            {
                                {
                                    "message", new SwaggerSchema
                                    {
                                        Type = SwaggerSchemaType.String,
                                        Title = "Message",
                                        Format = "string",
                                        Description = "Queue Message",
                                    }
                                }
                            }
                            
                        }
                    },
                },
            },
            Inputs = new SwaggerSchema
            {
                Type = SwaggerSchemaType.Object,
                Properties = new OrdinalDictionary<SwaggerSchema>
                {
                    {
                        "queueName", new SwaggerSchema
                        {
                            Type = SwaggerSchemaType.String,
                            Title = "queue name",
                            Description = "queue name",
                        }
                    }
                },
                Required = new string[]
                {
                    "queueName"
                },
            },
            Connector = ApiFactory.OperationApi,
            Trigger = TriggerType.Batch,
            Recurrence = new RecurrenceSetting
            {
                Type = RecurrenceType.None,
            },
        };

        /// <summary>
        /// The receive documents operation.
        /// </summary>
        public static readonly ServiceOperation Operation = new ServiceOperation
        {
            Name = "receiveMessage",
            Id = "receiveMessage",
            Type = "receiveMessage",
            Properties = new ServiceOperationProperties
            {
                Api = ApiFactory.OperationApi.GetFlattenedApi(),
                Summary = "receive message",
                Description = "receive message",
                Visibility = Visibility.Important,
                OperationType = OperationType.ServiceProvider,
                BrandColor = ApiFactory.BrandColor,
                IconUri = ApiFactory.IconUri,
                Trigger = TriggerType.Batch,
            },
        };
    }
}
