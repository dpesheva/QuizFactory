namespace QuizFactory.Data.Models
{
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AnswersDefinition")]
    public partial class AnswerDefinition : DeletableEntity, IDeletableEntity
    {
        public AnswerDefinition()
        {
            UsersAnswers = new HashSet<UsersAnswer>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        [Required]
        public int Position { get; set; }

        public virtual QuestionDefinition QuestionDefinition { get; set; }

        public virtual ICollection<UsersAnswer> UsersAnswers { get; set; }


    }
}
