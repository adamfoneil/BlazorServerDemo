using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public partial class Label
    {
        [NotMapped]
        public int OpenWorkItems { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
    }
}
