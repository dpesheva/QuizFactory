namespace QuizFactory.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;
    [Table("Categories")]
    public partial class Category : AuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DeletedOn{ get; set; }
    }
}
