namespace QuizFactory.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Common.Interfaces;

    public partial class UsersAnswer : DeletableEntity, IDeletableEntity
    {
        public int Id { get; set; }

        public virtual AnswerDefinition AnswerDefinition { get; set; }

        public virtual QuestionDefinition QuestionDefinition { get; set; }

        public virtual TakenQuiz TakenQuiz { get; set; }
    }
}