namespace QuizFactory.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;

    public class Vote : DeletableEntity, IDeletableEntity
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int QuizId { get; set; }
     
        [Required]
        [Range(0, 5)]
        public int Value { get; set; }
        
        public virtual QuizDefinition QuizDefinition { get; set; }

    }
}