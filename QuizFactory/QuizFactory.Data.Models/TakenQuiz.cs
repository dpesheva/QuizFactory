namespace QuizFactory.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;

    [Table("TakenQuizzes")]
    public partial class TakenQuiz : DeletableEntity, IDeletableEntity
    {
        public TakenQuiz()
        {
            this.UsersAnswers = new HashSet<UsersAnswer>();
        }

        public int Id { get; set; }

        public int QuizDefinitionId { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Range(0, 100)]
        public int Score { get; set; }

        public bool IsCompleted { get; set; }

        public virtual QuizDefinition QuizDefinition { get; set; }

        public virtual ICollection<UsersAnswer> UsersAnswers { get; set; }
    }
}