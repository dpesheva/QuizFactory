namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using QuizFactory.Data;
    using QuizFactory.Mvc.ViewModels;
    using QuizFactory.Mvc.ViewModels.Play;

    public class PlayController : BaseController
    {
        public PlayController(IQuizFactoryData data)
            : base(data)
        {
        }

        //GET : Play/PlayQuiz
        [HttpGet]
        public ActionResult PlayQuiz(int? id)
        {
            var quiz = this.db.QuizzesDefinitions
                .All()
                .Where(q => q.Id == id)
                .Project()
                .To<QuizPlayViewModel>()
                .FirstOrDefault();

            if (quiz == null)
            {
                return this.Redirect("Error"); // TODO 
            }

            return this.View(quiz);
        }

        // POST: Play/Start
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlayQuiz(int id, SelectedAnswersViewModel selectedAnswers)
        {
            // var result = ProcessSelectedAnswers(selectedAnswers);

            if (User.Identity.IsAuthenticated)
            {
                // SaveResult(selectedAnswers);
            }

            // TODO

            var quiz = this.db.QuizzesDefinitions
          .All()
          .Where(q => q.Id == id)
          .Project()
          .To<QuizPlayViewModel>()
          .FirstOrDefault();

            if (quiz == null)
            {
                return this.Redirect("Error"); // TODO 
            }
            TempData["results"] = selectedAnswers;
            return this.View("DisplayAnswers", quiz);

        }

        //public ActionResult GetQuestion(int quizId, int currentQuestion)
        //{
        //    var question = this.db.QuestionsDefinitions
        //                       .All()
        //                       .Where(q => q.QuizDefinition.Id == quizId)
        //                       .Select(QuestionViewModel.FromQuestionDefinition)
        //                       .FirstOrDefault(); // TODO get next question

        //    if (question == null)
        //    {
        //        return this.Redirect("Error"); // TODO 
        //    }

        //    return this.PartialView("_OpenQuestionPartial", question);
        //}
    }
}