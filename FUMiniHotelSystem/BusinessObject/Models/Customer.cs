using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Customer
    {
        public Customer()
        {
            BookingReservations = new HashSet<BookingReservation>();
        }

        public int CustomerId { get; set; }
        public string? CustomerFullName { get; set; }
        public string? Telephone { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? CustomerBirthday { get; set; }
        public bool? CustomerStatus { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<BookingReservation> BookingReservations { get; set; }
    }
}
