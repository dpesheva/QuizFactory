namespace QuizFactory.Mvc.Areas.Users.ViewModels
{
    using System;
    using System.Collections.Generic;
    using QuizFactory.Mvc.ViewModels;
    
    public interface IQuizViewModel
    {
        int Id { get; set; }

        string Title { get; set; }

        string Author { get; set; }

        DateTime CreatedOn { get; set; }

        decimal Rating { get; set; }

        bool IsPublic { get; set; }

        string Category { get; set; }

        int CategoryId { get; set; }

        string NumberQuestions { get; set; }

        ICollection<QuestionViewModel> Questions { get; set; }
    }
}
