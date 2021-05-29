using System;
using System.Collections.Generic;
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
        public ulong Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        [DefaultValue(null)]
        public DateTime? CreationDate { get; set; }
        [DefaultValue(null)]
        public DateTime? LastUpdateDate { get; set; }

        public ulong? CurrentHistoryId { get; set; }
        [ForeignKey("CurrentHistoryId")]
        public AccountHistory? CurrentHistory { get; set; }

        [InverseProperty("Account")]
        public ICollection<AccountHistory> AccountHistories { get; set; }
        public ICollection<AccountCatch> AccountCatches { get; set; }
        public ICollection<AccountCandy> AccountCandies { get; set; }
    }
}
