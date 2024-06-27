using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRoomRepository
    {
        IEnumerable<RoomInformation> GetAllRoom();
        RoomInformation? GetRoomInfoByID(string id);
        bool UpdateRoom(RoomInformation room);
        bool CreateRoom(RoomInformation room);
        List<RoomInformation> SearchRoom(string searchValue);
        bool DeleteRoom(int id);
        decimal? GetTotalPriceByListRoomId(List<int> RoomId);
        bool UpdateStatusRoom(List<int> RoomId);
        IList<RoomInformation> Search(string text);
        ICollection<RoomType> GetAllRommTypes();
    }
}
