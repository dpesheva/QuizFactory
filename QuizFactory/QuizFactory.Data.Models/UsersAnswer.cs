namespace QuizFactory.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;

    public partial class UsersAnswer : DeletableEntity, IDeletableEntity
    {
        [Key, Column(Order = 0)]
        public int AnswerDefinitionId { get; set; }

        [Key, Column(Order = 1)]
        public int TakenQuizId { get; set; }

        public virtual AnswerDefinition AnswerDefinition { get; set; }

        public virtual TakenQuiz TakenQuiz { get; set; }
    }
}