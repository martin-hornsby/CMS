using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Users.Data.Entities
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } //Note: The spec infers numeric type. It'll be better if this was a Sequential Guid

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        [ConcurrencyCheck]
        public DateTime LastUpdatedOn { get; set; }
    }
}
