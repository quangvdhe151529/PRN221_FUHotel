using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class RoomInformation
    {
        public RoomInformation()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public int RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public int? RoomTypeId { get; set; }
        public bool? RoomStatus { get; set; }
        public decimal? RoomPricePerDay { get; set; }

        public virtual RoomType? RoomType { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
