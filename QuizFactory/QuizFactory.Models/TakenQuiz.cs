namespace QuizFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TakenQuizzes")]
    public partial class TakenQuiz
    {
        public TakenQuiz()
        {
            UsersAnswers = new HashSet<UsersAnswer>();
        }

        public int Id { get; set; }

        public int QuizDefinitionId { get; set; }

        public ApplicationUser User { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Score { get; set; }

        public bool IsCompleted { get; set; }

        public virtual QuizDefinition QuizDefinition { get; set; }

        public virtual ICollection<UsersAnswer> UsersAnswers { get; set; }
    }
}
