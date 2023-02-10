using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoardGame1.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        public string GameName { get; set; }

        public int GamePrice { get; set; }

        [ForeignKey("Rentals")]

        public int RentalId { get; set; }
        public virtual Rental Rentals { get; set; }

        public ICollection<Membership> Memberships { get; set; }
    }

    public class GameDto
    {
        public int GameId { get; set; }
        public string GameName { get; set; }

        public int GamePrice { get; set; }
        public int RentalId { get; set; }

        public DateTime RentDate { get; set; }

    }
}