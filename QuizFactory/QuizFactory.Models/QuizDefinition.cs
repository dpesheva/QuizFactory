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
            QuestionsDefinitions = new HashSet<QuestionDefinition>();
            TakenQuizzes = new HashSet<TakenQuiz>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public ApplicationUser Author { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Rating { get; set; }

        public int CategoryId { get; set; }

        public bool IsPublic { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<QuestionDefinition> QuestionsDefinitions { get; set; }

        public virtual ICollection<TakenQuiz> TakenQuizzes { get; set; }
    }
}
