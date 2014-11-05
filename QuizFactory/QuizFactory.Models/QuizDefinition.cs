namespace QuizFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("QuizzesDefinition")]
    public partial class QuizDefinition
    {
        public QuizDefinition()
        {
            this.QuestionsDefinitions = new HashSet<QuestionDefinition>();
            this.TakenQuizzes = new HashSet<TakenQuiz>();
            this.IsActive = true;
            this.CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public ApplicationUser Author { get; set; }

        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool IsPublic { get; set; }

        public bool IsActive { get; set; }

        public DateTime UpdatedOn { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<QuestionDefinition> QuestionsDefinitions { get; set; }

        public virtual ICollection<TakenQuiz> TakenQuizzes { get; set; }
    }
}
