using Locker.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locker.Presentation.Controllers
{
    public class BaseController : Controller
    {
        public User LoggedUser { get; set; }
    }
}