﻿namespace QuizFactory.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using QuizFactory.Models;
    using QuizFactory.Mvc.Areas.Admin.ViewModels;

    public class QuizAdministrationController : AdminController
    {
        // GET: Admin/QuizAdministration
        public ActionResult Index()
        {
            var allQuizzes = this.db.QuizzesDefinitions
                                 .All()
                                 .Select(QuizAdminViewModel.FromQuizDefinition)
                                 .ToList();
            return this.View(allQuizzes);
        }

        // GET: Admin/QuizAdministration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizAdminViewModel quizAdminViewModel = this.GetViewModelById(id);

            if (quizAdminViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(quizAdminViewModel);
        }

        // GET: Admin/QuizAdministration/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Admin/QuizAdministration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,CreatedOn,Rating,IsPublic,IsDeleted,UpdatedOn,Category,NumberQuestions")]
                                   QuizAdminViewModel quizAdminViewModel)
        {
            if (this.ModelState.IsValid)
            {
                QuizDefinition newQuiz = new QuizDefinition();
                // TODO map view model to model
                this.db.QuizzesDefinitions.Add(newQuiz);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(quizAdminViewModel);
        }

        // GET: Admin/QuizAdministration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizAdminViewModel quizAdminViewModel = this.GetViewModelById(id);

            if (quizAdminViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(quizAdminViewModel);
        }

        // POST: Admin/QuizAdministration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,CreatedOn,Rating,IsPublic,IsDeleted,UpdatedOn,Category,NumberQuestions")]
                                 QuizAdminViewModel quizAdminViewModel)
        {
            if (this.ModelState.IsValid)
            {
                // TODO create new and disable the old
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(quizAdminViewModel);
        }

        // GET: Admin/QuizAdministration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizAdminViewModel quizAdminViewModel = this.GetViewModelById(id);

            if (quizAdminViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(quizAdminViewModel);
        }

        // POST: Admin/QuizAdministration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var quiz = this.db.QuizzesDefinitions
                           .SearchFor(q => q.Id == id)
                           .FirstOrDefault();

            quiz.IsDeleted = true;

            this.db.QuizzesDefinitions.Update(quiz);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private QuizAdminViewModel GetViewModelById(int? id)
        {
            QuizAdminViewModel quizAdminViewModel = this.db.QuizzesDefinitions
                                                        .SearchFor(q => q.Id == id)
                                                        .Select(QuizAdminViewModel.FromQuizDefinition)
                                                        .FirstOrDefault();
            return quizAdminViewModel;
        }
    }
}