using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains
{
    public class House
    {
        [Key]
        [Required]
        public string HouseId { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
