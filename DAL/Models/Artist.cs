using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Artist
    {
        #region " Properties "

        public int ArtistId { get; set; } // identity field

        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; } // not null

        public string? Name { get; set; }

        public string? Biography { get; set; }

        #endregion
    }
}
