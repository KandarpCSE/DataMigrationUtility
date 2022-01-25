using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmu_Console.Data.Models
{
    public class SourceModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public int FirstNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public int LastNumber { get; set; }
        public DestinationModel Destination { get; set; }
    }
}
