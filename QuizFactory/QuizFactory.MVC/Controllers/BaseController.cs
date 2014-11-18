namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Linq;  
    using System.Web.Mvc;
    using QuizFactory.Data;

    public abstract class BaseController : Controller
    {
        internal IQuizFactoryData Db;

        public BaseController(IQuizFactoryData data)
        {
            this.Db = data;
        }
    }
}