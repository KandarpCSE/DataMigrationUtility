using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmu_Console.Data.Models
{
    public class DestinationModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public int Sum { get; set; }

        public int  SourceModelId { get; set; }
        public SourceModel Source { get; set; }
    }
}
