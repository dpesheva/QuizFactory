namespace QuizFactory.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using QuizFactory.Mvc.Controllers;

    [Authorize(Roles = "admin")]
    public class AdminController : BaseController
    {
    }
}