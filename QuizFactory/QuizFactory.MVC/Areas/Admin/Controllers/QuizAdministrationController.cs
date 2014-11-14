namespace QuizFactory.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Areas.Admin.ViewModels;
    using QuizFactory.Mvc.Areas.Users.ViewModels;
    using QuizFactory.Mvc.Controllers;
    using System.Collections.Generic;

    [Authorize(Roles = "admin")]
    public class QuizAdministrationController : BaseController
    {
        public QuizAdministrationController(IQuizFactoryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            //var allQuizzes = this.db.QuizzesDefinitions
            //    .All()
            //    .Select(QuizAdminViewModel.FromQuizDefinition).ToList();

            //TempData["categories"] = this.db.Categories.All().Project().To<CategoryViewModel>().ToList();

            //return this.View(allQuizzes);

            return null;
        }

        public ICollection<CategoryViewModel> Categories()
        {
            return this.db.Categories.All().Project().To<CategoryViewModel>().ToList();
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            //var allQuizzes = this.db.QuizzesDefinitions
            //    .All()
            //    .Select(QuizAdminViewModel.FromQuizDefinition)
            //    .ToDataSourceResult(request);

            //return this.Json(allQuizzes);
            return null;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, QuizAdminViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var newQuiz = new QuizDefinition();
                MapViewModelToModel(model, newQuiz);

                this.db.QuizzesDefinitions.Add(newQuiz);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
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

            this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().ToList(), "Id", "Name", quizAdminViewModel.CategoryId);

            return this.View(quizAdminViewModel);
        }

        // POST: Admin/QuizAdministration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuizAdminViewModel quizAdminViewModel)
        {
            if (this.ModelState.IsValid)
            {
                // TODO create new and disable the old
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().ToList(), "Id", "Name", quizAdminViewModel.CategoryId);

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

        protected void MapViewModelToModel(QuizAdminViewModel quizViewModel, QuizDefinition quiz)
        {
            var category = this.db.Categories.SearchFor(c => c.Id == quizViewModel.CategoryId).FirstOrDefault();
            if (category == null)
            {
                return; // TODO
            }

            quiz.Title = quizViewModel.Title;
            quiz.CategoryId = quizViewModel.CategoryId;
            quiz.IsPublic = quizViewModel.IsPublic;
            quiz.Rating = quizViewModel.Rating;
        }


        private QuizAdminViewModel GetViewModelById(int? id)
        {
            //QuizAdminViewModel quizAdminViewModel = this.db.QuizzesDefinitions
            //                                            .SearchFor(q => q.Id == id)
            //                                            .Select(QuizAdminViewModel.FromQuizDefinition)
            //                                            .FirstOrDefault();
            //return quizAdminViewModel;

            return null;
        }
    }
}