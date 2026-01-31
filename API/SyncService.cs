using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class SyncService
{
    private HubConnection _connection;

    public event Action<string> OnMessageReceived;

    public SyncService(string hubUrl)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .WithAutomaticReconnect()
            .Build();

        _connection.On<string>("ReceiveUpdate", message =>
        {
            OnMessageReceived?.Invoke(message);
        });
    }

    public async Task StartAsync()
    {
        await _connection.StartAsync();
    }
    public void StartListening()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "syncQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Otrzymano wiadomość z kolejki: " + message);

            // Tutaj możesz np. odświeżyć dane w pamięci lub wysłać aktualizację do SignalR
        };

        channel.BasicConsume(queue: "syncQueue", autoAck: true, consumer: consumer);
    }
}
