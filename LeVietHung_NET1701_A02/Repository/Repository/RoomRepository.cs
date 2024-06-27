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

        public ICollection<RoomType> GetAllRommTypes()
        {
            return RoomDAO.Instance.GetAllRommTypes();
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

        public IList<RoomInformation> Search(string text)
        {
            IList<RoomInformation> rooms = RoomDAO.Instance.GetAllRoom().ToList();
            var searchString = text.ToLowerInvariant();

            var filteredRooms = rooms
                .Where(room =>
                    room.RoomId.ToString().Contains(searchString) ||
                    room.RoomNumber?.ToLowerInvariant().Contains(searchString) == true ||
                    room.RoomDetailDescription?.ToLowerInvariant().Contains(searchString) == true ||
                    room.RoomMaxCapacity?.ToString().Contains(searchString) == true ||
                    room.RoomTypeId.ToString().Contains(searchString) == true ||
                    room.RoomStatus?.ToString().Contains(searchString) == true ||
                    room.RoomPricePerDay?.ToString().Contains(searchString) == true
                )
                .ToList();

            return filteredRooms;
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
