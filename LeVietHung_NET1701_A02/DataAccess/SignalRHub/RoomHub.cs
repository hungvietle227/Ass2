using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace DataAccess.SignalRHub
{
    public class RoomHub : Hub
    {
        public async Task UpdateRoomStatus(int roomId, int status)
        {
            await Clients.All.SendAsync("ReceiveRoomUpdate", roomId, status);
        }
    }
}
