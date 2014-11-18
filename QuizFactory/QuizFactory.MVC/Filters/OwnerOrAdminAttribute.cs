namespace QuizFactory.Mvc.Filters
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Data.Common;
    using QuizFactory.Mvc.Controllers;

    public class OwnerOrAdminAttribute : AuthorizeAttribute
    {
        private readonly string routeParameter;

        public OwnerOrAdminAttribute(string parameter)
        {
            this.routeParameter = parameter;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthorized = false;
            if (filterContext.HttpContext.User.IsInRole(GlobalConstants.AdminRole))
            {
                isAuthorized = true;
            }
            else
            {
                var cntrl = (BaseController)filterContext.Controller;
                var uow = cntrl.Db;

                // first look at routedata then at request parameter:
                var idString = (filterContext.HttpContext.Request.RequestContext.RouteData.Values[this.routeParameter] as string) ??
                               filterContext.HttpContext.Request[this.routeParameter];

                int id;
                if (!int.TryParse(idString, out id))
                {
                    throw new HttpException("Incorrect quiz identifier");
                }

                var currentUser = filterContext.HttpContext.User.Identity.GetUserId();

                if (uow.QuizzesDefinitions.SearchFor(q => q.Id == id && q.Author.Id == currentUser).Any())
                {
                    isAuthorized = true;
                }
            }

            if (!isAuthorized)
            {
                throw new HttpException("The quiz is available for its owner or an administrator.");
                // filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}