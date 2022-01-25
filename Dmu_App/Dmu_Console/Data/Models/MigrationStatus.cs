using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmu_Console.Data.Models
{
    public class MigrationStatus
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public int From { get; set; }

        [Required]
        [MaxLength(100)]
        public int To { get; set; }

        [Required]
        [MaxLength(100)]
        public EnumValues Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        public TimeSpan ExecutionTime { get; set; }

    }
}
