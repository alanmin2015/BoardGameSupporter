using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGame1.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        public DateTime PurchaseDate { get; set; }

    }
}