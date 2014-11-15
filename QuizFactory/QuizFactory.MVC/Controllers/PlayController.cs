namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
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
        public ActionResult PlayQuiz(int id, Dictionary<string, string> questions)
        {
            Dictionary<int, int> selectedAnswersInt = ConvertToIntValues(questions);
            // TODO
            //   var result = ProcessSelectedAnswers(selectedAnswersInt);

            if (User.Identity.IsAuthenticated)
            {
                SaveResult(id, selectedAnswersInt);
            }

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

            TempData["results"] = selectedAnswersInt;
            return this.View("DisplayAnswers", quiz);
        }

        private int ProcessSelectedAnswers(Dictionary<int, int> selectedAnswers)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        private Dictionary<int, int> ConvertToIntValues(Dictionary<string, string> questions)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            foreach (var item in questions)
            {
                var answerId = int.Parse(item.Value);
                var questionId = this.db.AnswerDefinitions.Find(answerId).QuestionDefinition.Id;

                result.Add(questionId, answerId);
            }

            return result;
        }

        private void SaveResult(int quizId, Dictionary<int, int> selectedAnswers)
        {
            var user = User.Identity.GetUserId();
            TakenQuiz takenQuiz = new TakenQuiz();
            takenQuiz.UserId = user;
            takenQuiz.QuizDefinitionId = quizId;

            foreach (var item in selectedAnswers)
            {
                var answerId = item.Value;
                // var answer = this.db.AnswerDefinitions.Find(answerId);
                UsersAnswer givenAnswer = new UsersAnswer();
                givenAnswer.AnswerDefinitionId = answerId;
                givenAnswer.TakenQuiz = takenQuiz;
                db.UsersAnswers.Add(givenAnswer);
            }

            this.db.TakenQuizzes.Add(takenQuiz);
            this.db.SaveChanges();
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