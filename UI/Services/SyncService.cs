using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

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

    public async Task StartAsync() => await _connection.StartAsync();
    public async Task StopAsync() => await _connection.StopAsync();
}
