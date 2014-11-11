namespace QuizFactory.Mvc.Filters
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Mvc.Controllers;

    public class OwnerOrAdminAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthorized = false;
            if (filterContext.HttpContext.User.IsInRole("admin"))
            {
                isAuthorized = true;
            }
            else
            {
                var cntrl = (BaseController)filterContext.Controller;
                var uow = cntrl.db;

                // first look at routedata then at request parameter:
                var idString = (filterContext.HttpContext.Request.RequestContext.RouteData.Values["quizId"] as string) ??
                               filterContext.HttpContext.Request["quizId"];

                int id;
                if (!int.TryParse(idString, out id))
                {
                    throw new ArgumentException("Incorrect quiz identifier");
                }

                var currentUser = filterContext.HttpContext.User.Identity.GetUserId();

                if (uow.QuizzesDefinitions.SearchFor(q => q.Id == id && q.Author.Id == currentUser).Any())
                {
                    isAuthorized = true;
                }
            }

            if (!isAuthorized)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}