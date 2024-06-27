using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoomDAO
    {
        private static RoomDAO? instance;
        private static readonly object instanceLook = new object();
        public RoomDAO() { }
        public static RoomDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new RoomDAO();
                    }
                }
                return instance;
            }
        }
        public IEnumerable<RoomInformation> GetAllRoom()
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                return db.RoomInformations.Include(r => r.RoomType).Where(a => a.RoomStatus == 1).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public RoomInformation? GetRoomInfoByID(string id)
        {
            int.TryParse(id, out var ID);
            using var db = new FuminiHotelManagementContext();
            RoomInformation? roomInfo = db.RoomInformations.FirstOrDefault(c => c.RoomId == ID);
            return roomInfo;
        }
        public bool UpdateRoom(RoomInformation room)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.RoomInformations.Update(room);
                var result = db.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CreateRoom(RoomInformation room)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.RoomInformations.Add(room);
                var result = db.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RoomInformation> SearchRoom(string searchValue)
        {
            var listRoom = new List<RoomInformation>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                listRoom = db.RoomInformations.Where(a => a.RoomNumber == (searchValue) ||
                a.RoomMaxCapacity.ToString() == searchValue).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listRoom;
        }

        public bool DeleteRoom(int id)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var room = db.RoomInformations.FirstOrDefault(a => a.RoomId == id);
                db.RoomInformations.Remove(room);
                var result = db.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal? GetTotalPriceByListRoomId(List<int> RoomId)
        {
            decimal? totalPrice = 0;
            using var db = new FuminiHotelManagementContext();
            foreach (int roomId in RoomId)
            {
                totalPrice += db.RoomInformations.FirstOrDefault(r => r.RoomId == roomId).RoomPricePerDay;
            };
            return totalPrice;
        }

        public bool UpdateStatusRoom(List<int> RoomId)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                foreach (int roomId in RoomId)
                {
                    var room = db.RoomInformations.FirstOrDefault(r => r.RoomId == roomId);
                    room.RoomStatus = 0;
                    db.RoomInformations.Update(room);
                    var result = db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public ICollection<RoomType> GetAllRommTypes()
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                return db.RoomTypes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
