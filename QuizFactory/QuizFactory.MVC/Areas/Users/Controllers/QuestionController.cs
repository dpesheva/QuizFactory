using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizFactory.Data;
using QuizFactory.Mvc.ViewModels;
using QuizFactory.Mvc.Controllers;
using QuizFactory.Data.Models;

namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    // add custom attribute
    public class QuestionController : BaseController
    {
        public ActionResult Index(int? quizId)
        {
            if (quizId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.TempData["quizId"] = quizId;

            var allAquestions = this.db.QuestionsDefinitions
                .SearchFor(q => q.QuizDefinition.Id == quizId)
                .Select(QuestionViewModel.FromQuestionDefinition)
                .ToList();

            return View(allAquestions);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuestionViewModel questionViewModel = this.db.QuestionsDefinitions
                .SearchFor(q => q.Id == id)
                .Select(QuestionViewModel.FromQuestionDefinition)
                .FirstOrDefault();

            if (questionViewModel == null)
            {
                return HttpNotFound();
            }
            return View(questionViewModel);
        }

        // GET: Users/Question/Add
        public ActionResult Add(int? quizId)
        {
            return View();
        }

        // POST: Users/Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(QuestionViewModel questionViewModel, int? quizId)
        {
            var quiz = db.QuizzesDefinitions.Find(quizId);
            if (quiz == null)
            {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var newQuestion = new QuestionDefinition();

                MapFromModel(questionViewModel, newQuestion);
                newQuestion.QuizDefinition = quiz;

                db.QuestionsDefinitions.Add(newQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionViewModel);
        }

        // GET: Users/Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var questionViewModel = db.QuestionsDefinitions
                .SearchFor(q => q.Id == id)
                .Select(QuestionViewModel.FromQuestionDefinition)
                .FirstOrDefault();

            if (questionViewModel == null)
            {
                return HttpNotFound();
            }
            return View(questionViewModel);
        }

        // POST: Users/Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel questionViewModel, int? quizId)
        {
            if (ModelState.IsValid)
            {
               // TODO create new and set quiz id
                db.SaveChanges();
                return RedirectToAction("Index", new { quizId = quizId });
            }
            return View(questionViewModel);
        }

        // GET: Users/Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var questionViewModel = db.QuestionsDefinitions
                .SearchFor(q => q.Id == id)
                .Select(QuestionViewModel.FromQuestionDefinition)
                .FirstOrDefault();

            if (questionViewModel == null)
            {
                return HttpNotFound();
            }
            return View(questionViewModel);
        }

        // POST: Users/Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var question = db.QuestionsDefinitions.Find(id);
            var quizId = question.QuizDefinition.Id;
           
            db.QuestionsDefinitions.Delete(question);
            db.SaveChanges();
            return RedirectToAction("Index", new { quizId = quizId });
        }

        private void MapFromModel(QuestionViewModel questionViewModel, QuestionDefinition newQuestion)
        {
            newQuestion.QuestionText = questionViewModel.QuestionText;
            foreach (var item in newQuestion.AnswersDefinitions)
            {
                if (item != null)
                {
                    new AnswerDefinition()
                    {
                        Text = item.Text,
                        Position = item.Position,
                        QuestionDefinition = newQuestion
                    };

                    newQuestion.AnswersDefinitions.Add(item);
                }

            }
        }
    }
}
