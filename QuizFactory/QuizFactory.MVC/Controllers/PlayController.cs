namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.ViewModels.Play;
    using System.Web;

    public class PlayController : BaseController
    {
        public PlayController(IQuizFactoryData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult PlayQuiz(int? id)
        {
            var quiz = GetQuizById(id);

            if (quiz == null)
            {
                throw new HttpException("Quiz not found");
            }

            return this.View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlayQuiz(int id, Dictionary<string, string> questions)
        {
            int correctCount;
            Dictionary<int, int> selectedAnswersInt = this.ProcessResults(id, questions, out correctCount);

            var quiz = GetQuizById(id);

            if (quiz == null)
            {
                throw new HttpException("Quiz not found");
            }
            if (selectedAnswersInt.Count() != quiz.Questions.Count())
            {
                throw new HttpException("Incorrect number of answers ");
            }

            this.TempData["results"] = selectedAnswersInt;
            var scorePercentage = 100 * correctCount / quiz.Questions.Count();
            this.TempData["scorePercentage"] = scorePercentage;

            if (this.User.Identity.IsAuthenticated)
            {
                var takenQuizId = this.SaveResult(id, selectedAnswersInt, scorePercentage);
                return this.RedirectToAction("Details", "History", new { id = takenQuizId });
            }

            return this.RedirectToAction("DisplayAnswers", new { id = id });
        }

        public ActionResult DisplayAnswers(int id)
        {
            if (this.TempData["results"] == null)
            {
                return this.RedirectToAction("Index", "Home");
            }
            var quiz = GetQuizById(id);
            return this.View(quiz);
        }

        [Authorize]
        public ActionResult Vote(int value, int? id)
        {
            if (value < 1 || 5 < value || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           
            // TODO set to taken quiz, calc rate
            return this.View();
        }

        private Dictionary<int, int> ProcessResults(int quizId, Dictionary<string, string> questions, out int correctCount)
        {
            correctCount = 0;
            Dictionary<int, int> result = new Dictionary<int, int>();

            foreach (var item in questions)
            {
                var answerId = int.Parse(item.Value);

                var answer = this.db.AnswerDefinitions.Find(answerId);
                var question = answer.QuestionDefinition;

                if (question.QuizDefinition.Id != quizId)
                {
                    throw new HttpException("Incorrect question identifier");
                }

                if (answer.IsCorrect)
                {
                    correctCount++;
                }

                result.Add(question.Id, answerId);
            }

            return result;
        }

        private int SaveResult(int quizId, Dictionary<int, int> selectedAnswers, int scorePercentage)
        {
            var user = this.User.Identity.GetUserId();
            TakenQuiz takenQuiz = new TakenQuiz();
            takenQuiz.UserId = user;
            takenQuiz.QuizDefinitionId = quizId;

            foreach (var item in selectedAnswers)
            {
                var answerId = item.Value;

                UsersAnswer givenAnswer = new UsersAnswer();
                givenAnswer.AnswerDefinitionId = answerId;
                givenAnswer.TakenQuiz = takenQuiz;
                this.db.UsersAnswers.Add(givenAnswer);
            }

            takenQuiz.Score = scorePercentage;
            this.db.TakenQuizzes.Add(takenQuiz);
            this.db.SaveChanges();

            return takenQuiz.Id;
        }

        private QuizPlayViewModel GetQuizById(int? id)
        {
            if (id == null)
            {
                throw new HttpException("Wrong identifier");
            }

            var quiz = this.db.QuizzesDefinitions
                           .All()
                           .Where(q => q.Id == id)
                           .Project()
                           .To<QuizPlayViewModel>()
                           .FirstOrDefault();
            return quiz;
        }
    }
}