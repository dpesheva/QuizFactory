namespace QuizFactory.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using QuizFactory.Data;
    using QuizFactory.Data.Common.Interfaces;
    using QuizFactory.Mvc.Areas.Admin.ViewModels;
    using QuizFactory.Mvc.Areas.Common.Controllers;

    using Model = QuizFactory.Data.Models.QuizDefinition;
    using ViewModel = QuizFactory.Mvc.Areas.Admin.ViewModels.QuizAdminViewModel;

    [Authorize(Roles = "admin")]
    public class QuizAdminController : KendoGridAdministrationController
    {
        public QuizAdminController(IQuizFactoryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            this.TempData["categories"] = this.db.Categories.All().Project().To<CategoryViewModel>().ToList();
            return this.View();
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null)
            {
                model.Id = dbModel.Id;
            }
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]
                                    DataSourceRequest request, ViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                this.db.QuizzesDefinitions.Delete(model.Id);
                this.db.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            try
            {
                return this.db.QuizzesDefinitions
                    .All()
                    .Project()
                    .To<QuizAdminViewModel>()
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override T GetById<T>(object id)
        {
            return this.db.QuizzesDefinitions.Find(id) as T;
        }
    }
}