using DotPulsar;
using DotPulsar.Extensions;
using GeneracionOrdenDespacho.EventHandlers;
using GeneracionOrdenDespacho.ViewModels;
using System.Buffers;
using System.Text;
using System.Text.Json;

namespace GeneracionOrdenDespacho
{
    internal static class HelperPulsarBroker
    {
        private static IConfiguration _configuration;

        internal static async void ConsumeMessages<T>(IConfiguration configuration, string nombreTopico)//string serviceUrl, string topic, string subscription, string databaseConnection)
        {
            _configuration = configuration;
            var client = PulsarClient.Builder().ServiceUrl(new Uri(_configuration["Pulsar:Uri"])).Build();
            var consumer = client.NewConsumer()
                     .SubscriptionName(_configuration["Pulsar:Subscription"])
                     .Topic(nombreTopico)
                     .Create();

            await foreach (var message in consumer.Messages())
            {
                Task.Run(() =>
                {
                    try
                    {
                        var messageContentsStr = Encoding.UTF8.GetString(message.Data.ToArray());
                        var messageContents = JsonSerializer.Deserialize<EventoInventarioVerificado>(messageContentsStr);
                        Console.WriteLine($"Recibido el mensaje: {messageContents}, se envía al despachador");
                        var dispatcher = new EventDispatcher();
                        dispatcher.Dispatch(messageContents, _configuration);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                });
            }

        }

        internal static async Task<string> SendMessage(string serviceUrl, string topic, string subscription, object payload)
        {
            var client = PulsarClient.Builder().ServiceUrl(new Uri(serviceUrl)).Build();
            var producer = client.NewProducer()
                     .Topic(topic)
                     .Create();
            var messageId = await producer.Send(JsonSerializer.SerializeToUtf8Bytes(payload));
            return messageId.ToString();
        }
    }
}
