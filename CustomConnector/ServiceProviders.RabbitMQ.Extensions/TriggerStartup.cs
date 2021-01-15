using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;

[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(ServiceProviders.RabbitMQ.Extensions.TriggerStartup))]

namespace ServiceProviders.RabbitMQ.Extensions
{
    public class TriggerStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<ServiceProvider>();
            builder.Services.TryAddSingleton<RabbitMQServiceOperationProvider>();
        }
    }
}
