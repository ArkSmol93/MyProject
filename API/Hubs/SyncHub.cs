using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class SyncHub : Hub
    {
        // Przykładowa metoda wysyłająca komunikat do wszystkich klientów
        public async Task SendUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }

        public async Task NotifyChange(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }
    }
}
