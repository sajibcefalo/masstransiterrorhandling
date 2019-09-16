using System.Threading.Tasks;
using MassTransit;
using MasstransitErrorHandling.Models;


namespace MasstransitErrorHandling.Consumers
{
    public class CommonFaultConsumer : IConsumer<Fault<OrderCreated>>
    {
        public Task Consume(ConsumeContext<Fault<OrderCreated>> context)
        {
            // log exception etc...
            return Task.CompletedTask;
        }
    }
}
