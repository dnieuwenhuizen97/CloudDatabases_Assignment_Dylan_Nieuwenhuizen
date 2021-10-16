using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class MortgageOffer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string MortgageOfferId { get; set; }

        [Required]
        public double MaxAmountToBorrow { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }
    }
}
