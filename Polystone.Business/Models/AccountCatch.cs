using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polystone.Business.Models
{
    [Table("AccountCatch")]
    public class AccountCatch
    {
        [Key]
        public ulong Id { get; set; }
        [DefaultValue(-1)]
        public int Specie { get; set; }
        [DefaultValue(null)]
        public DateTime? CatchDate { get; set; }
        public int Cp { get; set; }
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
