using System.ComponentModel.DataAnnotations.Schema;

namespace MarketplaceBackend.Models
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
