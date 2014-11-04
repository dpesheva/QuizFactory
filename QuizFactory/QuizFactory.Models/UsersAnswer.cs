namespace QuizFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class UsersAnswer
    {
        public int Id { get; set; }

        //[Key]
        //[Column(Order = 1)]
        //public int TakenQuizId { get; set; }

        //[Key]
        //[Column(Order = 2)]
        //public int QuestionDefinitionId { get; set; }

        //[Key]
        //[Column("AnswerDefinitionId", Order = 3)]
        //public int GivenAnswerId { get; set; }

        public virtual AnswerDefinition AnswerDefinition { get; set; }

        public virtual QuestionDefinition QuestionDefinition { get; set; }

        public virtual TakenQuiz TakenQuiz { get; set; }
    }
}
