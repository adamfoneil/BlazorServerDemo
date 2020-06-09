using Models.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CloseReason : AppTable
    {
        [Key]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
