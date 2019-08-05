using Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Controllers
{
    public partial class OtzivController : Controller
    {
        //
        // GET: /Otziv/

        public virtual ActionResult Index(int page, int count)
        {
            OtzivList model = new OtzivList(page,count);
            db.GetOtziv(ref model);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult SendOtziv(string Text, int Page = 1)
        {
            db.SendOtziv(Text,User.Identity.Name);
            return RedirectToAction("Index", new { page = Page, count = 10});
        }

        public bool HideOtziv(int id,int val)
        {
            return (db.HideOtziv(id, val));
        }
    }
}
