using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using Xunit;

public class SyncHubTests
{
    [Fact]
    public async Task Hub_ReceivesMessage()
    {
        var tcs = new TaskCompletionSource<string>();

        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:5001/syncHub") // dopasuj do portu API
            .WithAutomaticReconnect()
            .Build();

        connection.On<string>("ReceiveUpdate", msg =>
        {
            tcs.TrySetResult(msg);
        });

        await connection.StartAsync();
        await connection.InvokeAsync("NotifyChange", "Test message");

        // Poczekaj na wiadomość lub timeout
        var receivedMessage = await Task.WhenAny(tcs.Task, Task.Delay(2000)) == tcs.Task
            ? tcs.Task.Result
            : throw new TimeoutException("Nie otrzymano wiadomości w czasie testu");

        Assert.Equal("Test message", receivedMessage);
    }
}
