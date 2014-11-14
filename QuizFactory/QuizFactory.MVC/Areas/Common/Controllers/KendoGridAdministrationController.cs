namespace QuizFactory.Mvc.Areas.Common.Controllers
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using QuizFactory.Data;
    using QuizFactory.Data.Common;
    using QuizFactory.Mvc.Areas.Common.ViewModels;
    using QuizFactory.Mvc.Controllers;

    public abstract class KendoGridAdministrationController : BaseController
    {
        public KendoGridAdministrationController(IQuizFactoryData data)
            : base(data)
        {
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var ads = this.GetData().ToDataSourceResult(request);

            return this.Json(ads);
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : AuditInfo
            where TViewModel : QuizViewModel
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                try
                {
                    Mapper.Map<TViewModel, TModel>(model, dbModel);
                }
                catch (Exception ex)
                {

                }

                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
               // model.ModifiedOn = dbModel.ModifiedOn;
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest] DataSourceRequest request)
        {
            return this.Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.db.Context.Entry(dbModel);
            entry.State = state;
            this.db.SaveChanges();
        }
    }
}