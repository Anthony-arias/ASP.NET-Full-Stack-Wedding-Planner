using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Wedding_Planner.Models
{
    public class WeddingUserAssociation
    {
        [Key]
        public int AssociationId { get; set; }

        [Required]
        public int WeddingId { get; set; }
        public Wedding Wedding { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
