using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoomRepository : IRoomRepository
    {
        public bool CreateRoom(RoomInformation room)
        {
            return RoomDAO.Instance.CreateRoom(room);
        }

        public bool DeleteRoom(int id)
        {
            return RoomDAO.Instance.DeleteRoom(id);
        }

        public IEnumerable<RoomInformation> GetAllRoom()
        {
            return RoomDAO.Instance.GetAllRoom();
        }

        public RoomInformation? GetRoomInfoByID(string id)
        {
            return RoomDAO.Instance.GetRoomInfoByID(id);
        }

        public decimal? GetTotalPriceByListRoomId(List<int> RoomId)
        {
            return RoomDAO.Instance.GetTotalPriceByListRoomId(RoomId);
        }

        public List<RoomInformation> SearchRoom(string searchValue)
        {
            return RoomDAO.Instance.SearchRoom(searchValue);
        }

        public bool UpdateRoom(RoomInformation room)
        {
            return RoomDAO.Instance.UpdateRoom(room);
        }

        public bool UpdateStatusRoom(List<int> RoomId)
        {
            return RoomDAO.Instance.UpdateStatusRoom(RoomId);
        }
    }
}
