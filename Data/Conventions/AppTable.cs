using AO.Models;

namespace Models.Conventions
{
    [Schema("app")]
    [Identity(nameof(Id))]
    public abstract class AppTable
    {
        public int Id { get; set; }
    }
}
