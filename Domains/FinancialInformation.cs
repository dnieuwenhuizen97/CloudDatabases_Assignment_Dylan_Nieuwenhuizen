using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class FinancialInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FinancialInformationId { get; set; }

        [Required]
        public double AnnualSalary { get; set; }

        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
