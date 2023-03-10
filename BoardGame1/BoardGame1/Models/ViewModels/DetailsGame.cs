using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGame1.Models.ViewModels
{
    public class DetailsGame
    {
        public GameDto SelectedGame { get; set; }
        public IEnumerable<MembershipDto> RelatedMemberships { get; set; }

        public IEnumerable<MembershipDto> AvailableMemberships { get; set; }
    }
}