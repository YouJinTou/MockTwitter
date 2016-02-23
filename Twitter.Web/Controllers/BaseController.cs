using System.Web.Mvc;
using Twitter.Data.UnitOfWork;

namespace Twitter.Web.Controllers
{
    public class BaseController : Controller
    {
        // Not sure if this is a security vulnerability, but this saves a lot of trouble when
        // a page refresh is required as there are some issues that arise if RedirectToAction is used
        protected const string ReloadScript =
           @"<script language='javascript' type='text/javascript'>location.reload(true);</script>";

        protected ITwitterData data;

        public BaseController(ITwitterData data)
        {
            this.data = data;
        }

        public ITwitterData Data { get; private set; }
    }
}