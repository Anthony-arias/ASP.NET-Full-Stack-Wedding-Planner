using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wedding_Planner.Controllers;

namespace Wedding_Planner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }
        [Required]
        public int CreatedById { get; set; }
        [Required]
        public string WedderOne { get; set; }
        [Required]
        public string WedderTwo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime WeddingDate { get; set; }

        [Required]
        public string WeddingAdress { get; set; }

        public List<WeddingUserAssociation> Attendees { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
