using DotPulsar;
using DotPulsar.Extensions;
using System.Text.Json;

namespace BFFProgramarDespacho
{
    internal static class HelperPulsarBroker
    {
        private static IConfiguration _configuration;

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
