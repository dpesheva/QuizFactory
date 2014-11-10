namespace QuizFactory.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;

    [Table("QuestionsDefinition")]
    public partial class QuestionDefinition: AuditInfo, IDeletableEntity
    {
        public QuestionDefinition()
        {
            this.AnswersDefinitions = new HashSet<AnswerDefinition>();
            this.UsersAnswers = new HashSet<UsersAnswer>();
        }

        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DeletedOn{ get; set; }
                
		[Required]
        public int Number { get; set; }

        public virtual QuizDefinition QuizDefinition { get; set; }

        public virtual ICollection<AnswerDefinition> AnswersDefinitions { get; set; }

        public virtual ICollection<UsersAnswer> UsersAnswers { get; set; }
    }
}