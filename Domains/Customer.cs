using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CustomerId { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public virtual FinancialInformation FinancialInformation { get; set; }

        public virtual ICollection<MortgageOffer> MortgageOffers { get; set; }
    }
}
