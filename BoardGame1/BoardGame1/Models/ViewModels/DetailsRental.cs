using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGame1.Models.ViewModels
{
    public class DetailsRental
    {
        public RentalDto SelectedRental { get; set; }
        public IEnumerable<GameDto> RelatedGames { get; set; }
    }
}