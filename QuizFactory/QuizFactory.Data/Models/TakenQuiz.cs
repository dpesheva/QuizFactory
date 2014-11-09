namespace QuizFactory.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;

    [Table("TakenQuizzes")]
    public partial class TakenQuiz : AuditInfo, IDeletableEntity
    {
        public TakenQuiz()
        {
            UsersAnswers = new HashSet<UsersAnswer>();
        }

        public int Id { get; set; }

        public int QuizDefinitionId { get; set; }

        public ApplicationUser User { get; set; }
       
        [Index]
        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Score { get; set; }

        public bool IsCompleted { get; set; }

        public virtual QuizDefinition QuizDefinition { get; set; }

        public virtual ICollection<UsersAnswer> UsersAnswers { get; set; }
    }
}
