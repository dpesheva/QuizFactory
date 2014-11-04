namespace QuizFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  
    [Table("Categories")]
    public partial class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }
    }
}
