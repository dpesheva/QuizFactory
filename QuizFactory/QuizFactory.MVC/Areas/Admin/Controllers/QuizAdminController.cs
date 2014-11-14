using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using QuizFactory.Data;
using QuizFactory.Mvc.Areas.Common.Controllers;

namespace QuizFactory.Mvc.Areas.Admin.Controllers
{
    using Model = QuizFactory.Data.Models.QuizDefinition;
    using ViewModel = QuizFactory.Mvc.Areas.Admin.ViewModels.QuizAdminViewModel;

    public class QuizAdminController : KendoGridAdministrationController
    {
          public QuizAdminController(IQuizFactoryData data)
            : base(data)
        {
        }

          public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.db.QuizzesDefinitions.All();
        }

        protected override T GetById<T>(object id)
        {
            return this.db.QuizzesDefinitions.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.db.QuizzesDefinitions.Delete(model.Id.Value);
                this.db.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}