namespace QuizFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("QuestionsDefinition")]
    public partial class QuestionDefinition
    {
        public QuestionDefinition()
        {
            this.AnswersDefinitions = new HashSet<AnswerDefinition>();
            this.UsersAnswers = new HashSet<UsersAnswer>();
        }

        [Column("QuestionDefinitionId")]
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }
        
        public virtual QuizDefinition QuizDefinition { get; set; }

        public virtual ICollection<AnswerDefinition> AnswersDefinitions { get; set; }

        public virtual ICollection<UsersAnswer> UsersAnswers { get; set; }
    }
}