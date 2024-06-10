namespace DataAccess.DTO
{
    public class CustomeRoom
    {
        public int RoomId { get; set; }

        public string RoomNumber { get; set; }

        public string? RoomDetailDescription { get; set; }

        public int? RoomMaxCapacity { get; set; }

        public string RoomTypeName { get; set; }

        public string? RoomStatus { get; set; }

        public decimal? RoomPricePerDay { get; set; }
    }
}