using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Memoryleek
{
    public static class RabbitMQTriggerTest
    {
        [FunctionName("RabbitMQTriggerTest")]
        public static void Run([RabbitMQTrigger("demo", ConnectionStringSetting = "RabbitMQConnectionAppSetting")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# RabbitMQ trigger function processed message: {myQueueItem}");
        }
    }
}
