﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Total
    {
        public int ArtistId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int TotalDiscs { get; set; }

        [Required]
        public decimal TotalCost { get; set; }
    }
}
