using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polystone.Business.Models
{
    [Table("AccountHistory")]
    public class AccountHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public int Level { get; set; }
        public long Experience { get; set; }
        public int Pokecoin { get; set; }
        public int Stardust { get; set; }
        public int PokemonCaught { get; set; }
        public int PokestopSpinned { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public ulong AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
