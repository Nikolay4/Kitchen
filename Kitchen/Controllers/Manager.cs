using Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Controllers
{
    public partial class ManagerController : Controller
    {
        public virtual ActionResult GetList()
        {
            return View(db.GetZakazList());
        }

        [HttpPost]
        public virtual ActionResult Change(Manager model, int Table)
        {
            db.EditZakaz(model,Table);
            return RedirectToAction("GetList", "Manager");
        }

        public virtual ActionResult TakeInWork(int Id, int Table)
        {
            int ManagerId = int.Parse(User.Identity.Name);
            db.TakeInWork(Id, Table, ManagerId);
            return RedirectToAction("GetList", "Manager");
        }
    }
}
