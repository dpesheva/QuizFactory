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
            int correctCount;
            Dictionary<int, int> selectedAnswersInt = ProcessResults(id, questions, out correctCount);


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
            if (selectedAnswersInt.Count() != quiz.Questions.Count())
            {
                return this.Redirect("Error"); // TODO			
            }

            TempData["results"] = selectedAnswersInt;
            var scorePercentage = 100 * correctCount / quiz.Questions.Count();
            TempData["scorePercentage"] = scorePercentage;

            if (User.Identity.IsAuthenticated)
            {
                SaveResult(id, selectedAnswersInt, scorePercentage);
            }

            return this.View("DisplayAnswers", quiz);
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
                    // TODO: frontend error or malicious user			
                    throw new Exception();
                }

                if (answer.IsCorrect)
                {
                    correctCount++;
                }


                result.Add(question.Id, answerId);
            }

            return result;
        }

        private void SaveResult(int quizId, Dictionary<int, int> selectedAnswers, int scorePercentage)
        {
            var user = User.Identity.GetUserId();
            TakenQuiz takenQuiz = new TakenQuiz();
            takenQuiz.UserId = user;
            takenQuiz.QuizDefinitionId = quizId;

            foreach (var item in selectedAnswers)
            {
                var answerId = item.Value;
                
                UsersAnswer givenAnswer = new UsersAnswer();
                givenAnswer.AnswerDefinitionId = answerId;
                givenAnswer.TakenQuiz = takenQuiz;
                db.UsersAnswers.Add(givenAnswer);
            }

            takenQuiz.Score = scorePercentage;
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