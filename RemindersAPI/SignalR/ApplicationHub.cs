using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RemindersAPI.SignalR
{
    public class ApplicationHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync(message);
        }
    }
}
