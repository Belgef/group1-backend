using System.ComponentModel.DataAnnotations.Schema;

namespace MarketplaceBackend.Models
{
    public enum Role { Admin, User }
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public Role Role { get; set; }
        public string AvatarUrl { get; set; }
    }
}
