using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polystone.Business.Models
{
    [Table("AccountCandy")]
    public class AccountCandy
    {
        [Key]
        public int Specie { get; set; }
        public int SmallCandy { get; set; }
        public int XLCandy { get; set; }

        public Account Account { get; set; }
    }
}
