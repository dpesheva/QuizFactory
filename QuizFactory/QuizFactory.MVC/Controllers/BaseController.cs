using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizFactory.Data;

namespace QuizFactory.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected IQuizFactoryData db;

        public BaseController(IQuizFactoryData data)
        {
            this.db = data;
        }

        public BaseController()
            : this(new QuizFactoryData())
        {
        }
    }
}