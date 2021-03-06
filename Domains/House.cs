using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains
{
    public class House
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string HouseId { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
