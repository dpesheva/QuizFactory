namespace QuizFactory.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;

    [Table("QuestionsDefinition")]
    public partial class QuestionDefinition : DeletableEntity, IDeletableEntity
    {
        public QuestionDefinition()
        {
            this.AnswersDefinitions = new HashSet<AnswerDefinition>();
        }

        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public int Number { get; set; }

        public virtual QuizDefinition QuizDefinition { get; set; }

        public virtual ICollection<AnswerDefinition> AnswersDefinitions { get; set; }
    }
}