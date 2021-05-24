using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polystone.Business.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(64)]
        public string HashedName { get; set; }

        [DefaultValue(0)]
        public int TotalXp { get; set; }

        [DefaultValue(0)]
        public int TotalStardust { get; set; }

        [DefaultValue(0)]
        public int CaughtPokemons { get; set; }

        [DefaultValue(0)]
        public int EscapedPokemons { get; set; }

        [DefaultValue(0)]
        public int ShinyPokemons { get; set; }

        [DefaultValue(0)]
        public int Pokestops { get; set; }

        [DefaultValue(0)]
        public int Rockets { get; set; }

        [DefaultValue(0)]
        public int Raids { get; set; }
    }
}
