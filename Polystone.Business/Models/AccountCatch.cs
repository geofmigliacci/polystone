using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POGOProtos.Rpc;

namespace Polystone.Business.Models
{
    [Table("AccountCatch")]
    public class AccountCatch
    {
        [Key]
        public ulong Id { get; set; }
        [DefaultValue(-1)]
        public int PokemonId { get; set; }
        public DateTime? CreationTimeMs { get; set; }
        public int Cp { get; set; }
        public int? IndividualAttack { get; set; }
        public int? IndividualDefense { get; set; }
        public int? IndividualStamina { get; set; }
        public HoloPokemonMove? Move1 { get; set; }
        public HoloPokemonMove? Move2 { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required]
        public int Stardust { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsShiny { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsShadow { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Account Account { get; set; }
    }
}
