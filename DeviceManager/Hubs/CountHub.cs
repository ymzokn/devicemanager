using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DeviceManager.Hubs
{
    public class CountHub : Hub
    {
        public async Task SendCount(string count)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveCount", count);
            }
            catch (Exception ex) 
            { 
                // Console.WriteLine("Waiting for clients");
            }
        }
    }
}
