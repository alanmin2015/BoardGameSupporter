using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGame1.Models.ViewModels
{
    public class DetailsMembership
    {
        public MembershipDto SelectedMembership { get; set; }
        public IEnumerable<GameDto> KeptGames { get; set; }
    }
}