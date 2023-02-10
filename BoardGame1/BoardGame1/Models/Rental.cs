using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGame1.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }

        public DateTime RentDate { get; set; }
    }

    public class RentalDto
    {
        public int RentalId { get; set; }

        public DateTime RentDate { get; set; }

    }
}