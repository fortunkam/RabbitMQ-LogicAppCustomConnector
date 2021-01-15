using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.Azure.Workflows.ServiceProviders.WebJobs.Abstractions.Providers;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Extensions;
using Microsoft.WindowsAzure.ResourceStack.Common.Storage.Cosmos;
using Microsoft.WindowsAzure.ResourceStack.Common.Swagger.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServiceProviders.RabbitMQ.Extensions
{
    [ServiceOperationsProvider(Id = ApiFactory.ServiceId, Name = ApiFactory.ServiceName)]
    public class RabbitMQServiceOperationProvider : IServiceOperationsTriggerProvider
    {
        /// <summary>
        /// Gets or sets service Operations.
        /// </summary>
        private readonly List<ServiceOperation> serviceOperationsList;

        /// <summary>
        /// The set of all API Operations.
        /// </summary>
        private readonly InsensitiveDictionary<ServiceOperation> apiOperationsList;

        public RabbitMQServiceOperationProvider()
        {
            this.serviceOperationsList = new List<ServiceOperation>();
            this.apiOperationsList = new InsensitiveDictionary<ServiceOperation>();

            this.apiOperationsList.AddRange(new InsensitiveDictionary<ServiceOperation>
            {
                { "receiveMessage", ReceiveMessage.Operation },
                //{ "debugTest", DebugTest.Operation }
            });

            this.serviceOperationsList.AddRange(new List<ServiceOperation>
            {
                { ReceiveMessage.Operation.CloneWithManifest(ReceiveMessage.Manifiest
                    ) },
                //{ DebugTest.Operation.CloneWithManifest(DebugTest.Manifiest
                //    ) },
            });
        }

        public string GetBindingConnectionInformation(string operationId, InsensitiveDictionary<JToken> connectionParameters)
        {
            var val = ServiceOperationsProviderUtilities
                   .GetRequiredParameterValue(
                       serviceId: ApiFactory.ServiceId,
                       operationId: operationId,
                       parameterName: "connectionString",
                       parameters: connectionParameters)?
                   .ToValue<string>();
            return val;
        }

        public string GetFunctionTriggerType()
        {
            return "RabbitMQTrigger";
        }

        public IEnumerable<ServiceOperation> GetOperations(bool expandManifest)
        {
            return expandManifest ? serviceOperationsList : GetApiOperations();
        }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        private IEnumerable<ServiceOperation> GetApiOperations()
        {
            return this.apiOperationsList.Values;
        }

        public ServiceOperationApi GetService()
        {
            return ApiFactory.OperationApi;
        }

        public Task<ServiceOperationResponse> InvokeActionOperation(string operationId, InsensitiveDictionary<JToken> connectionParameters, ServiceOperationRequest serviceOperationRequest)
        {
            return Task.Run(() =>
            {
                return new ServiceOperationResponse(new JObject
                {
                    { "outputParam",  serviceOperationRequest.Parameters["inputParam"].ToString() },
                    { "operationId",  operationId }

                });
            });
        }
    }
}
