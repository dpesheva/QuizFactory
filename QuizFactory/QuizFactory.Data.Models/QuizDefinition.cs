namespace QuizFactory.Data.Models
{
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("QuizzesDefinition")]
    public partial class QuizDefinition : DeletableEntity, IDeletableEntity
    {
        public QuizDefinition()
        {
            this.QuestionsDefinitions = new HashSet<QuestionDefinition>();
            this.TakenQuizzes = new HashSet<TakenQuiz>();
            this.Rating = 0m;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public ApplicationUser Author { get; set; }

        [Range(0, 5)]
        public decimal Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool IsPublic { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<QuestionDefinition> QuestionsDefinitions { get; set; }

        public virtual ICollection<TakenQuiz> TakenQuizzes { get; set; }
    }
}
