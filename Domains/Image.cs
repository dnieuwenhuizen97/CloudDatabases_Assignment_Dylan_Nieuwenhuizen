using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ImageKey { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        public virtual House House { get; set; }

        public Image(string imageLink)
        {
            ImageLink = imageLink;
        }
    }
}
