using Kitchen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Controllers
{
    public partial class NewsController : Controller
    {
        public virtual ActionResult Index(string alias)
        {
            return View(db.GetContent(alias));
        }

        public virtual ActionResult ListNews(int page = 1)
        {
            return View(db.getListNews(true, page));
        }

        public virtual ActionResult noPublishedList()
        {
            return View(db.getListNews(false, 1));
        }

        [HttpGet]
        public virtual ActionResult Edit(string alias = "")
        {
            return View(db.GetContent(alias));
        }

        [HttpPost]
        //[Authorize(Roles = "admin,news")]
        [ValidateInput(false)]
        public virtual ActionResult Edit(Models.ContentModel model)
        {
            if (model.Date == new DateTime()) model.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (model.id == null || model.id == 0)
                {
                    int res = db.SetContent(model);
                    if (res == -1) ModelState.AddModelError("alias", "новость с таким именем уже существует");
                    else model.id = res;
                }
                else db.UpdateContent(model);
            }
            return View(model);
        }

        [HttpPost]
        public int AddImage(HttpPostedFileBase image)
        {
            //HttpPostedFileBase image = Request.Files["fileInput"];
            byte[] buffer = null;
            using (var binaryReader = new BinaryReader(image.InputStream))
            {
                buffer = binaryReader.ReadBytes(image.ContentLength);
            }
            return db.addImg(0, buffer);;
        }
    }
}
