using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRoomRepository
    {
        IEnumerable<RoomInformation> GetAllRoom();
        RoomInformation? GetRoomInfoByID(string id);
        bool UpdateRoom(RoomInformation room);
        bool CreateRoom(RoomInformation room);
        List<RoomInformation> SearchRoom(string searchValue);
        bool DeleteRoom(int id);
    }
}
