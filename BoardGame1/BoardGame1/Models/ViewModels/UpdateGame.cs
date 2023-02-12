﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGame1.Models.ViewModels
{
    public class UpdateGame
    {
        public GameDto SelectedGame { get; set; }
        public IEnumerable<RentalDto> RentalOptions { get; set; }
    }
}