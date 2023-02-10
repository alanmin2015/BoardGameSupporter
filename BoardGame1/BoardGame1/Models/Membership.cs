using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGame1.Models
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}