using Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Controllers
{
    public partial class RaschetController : Controller
    {
        [Authorize]
        public virtual ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize]
        public virtual ActionResult SendZakaz(RaschetModels model)
        {
            model.UserId = int.Parse(User.Identity.Name);
            ViewData["result"] = (bool)db.Set_Raschet(model);
            return View();
        }

    }
}
