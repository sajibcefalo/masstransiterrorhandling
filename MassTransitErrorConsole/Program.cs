using System;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using MassTransit.Util;
using MasstransitErrorHandling.Models;
using Microsoft.Azure.ServiceBus.Primitives;

namespace MassTransitErrorConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var bus = Bus.Factory.CreateUsingAzureServiceBus(
                cfg =>
                {
                    var host = cfg.Host(
                        new Uri("<service bus uri>"),
                        h =>
                        {
                            h.OperationTimeout = TimeSpan.FromSeconds(5);
                            h.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("<Shared Access Key Name>", "<Shared Access Key>", TimeSpan.FromDays(180), TokenScope.Namespace);
                        });

                });

            TaskUtil.Await(() => bus.StartAsync());

            var sendEndpoint =TaskUtil.Await(async ()=>await bus.GetSendEndpoint(new Uri("<service bus uri>")));


            TaskUtil.Await(async () => await sendEndpoint.Send(new OrderCreated()
            {
                OrderId = "6546579875"
            }, typeof(OrderCreated)));

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }
}
