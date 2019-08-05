using Kitchen.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Kitchen.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize(Roles = "admin")]
        public virtual ActionResult Edit(int id = 0)
        {
            return View(db.GetThing(id));
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        [ValidateInput(false)]
        public virtual ActionResult Edit(Models.addKitchenModels model)
        {
            //var aa = ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
            if (ModelState.IsValid)
            {
                if (model.Id == null || model.Id == 0) model.Id = db.SetThing(model);
                else db.UpdateThing(model);
            }



            //if (model.AddImage != null && model.AddImage.ContentLength > 0)
            //{
            //    //получаем фотку
            //    byte[] buffer = null;
            //    using (var binaryReader = new BinaryReader(model.AddImage.InputStream))
            //    {
            //        buffer = binaryReader.ReadBytes(model.AddImage.ContentLength);
            //    }

            //    //сохраняем в базу
            //    string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            //    if (string.IsNullOrEmpty(myConnString)) return View();

            //    //string queryString = String.Format(@"Insert into Images (ImageId, Image) VALUES(1,'{0}')", buffer);
            //    string queryString = String.Format(@"Insert into Images (Image) VALUES(@img)");
            //    SqlConnection conn = new SqlConnection(myConnString);
            //    SqlCommand command = new SqlCommand(queryString, conn);

            //    command.Parameters.Add("@img", SqlDbType.Binary);
            //    command.Parameters["@img"].Value = buffer;

            //    conn.Open();
            //    command.ExecuteNonQuery();
            //    conn.Close();
            //}


            return View(model);
        }

        [MvcSiteMapNode(Title = "А", ParentKey = "Home")]
        public virtual ActionResult KitchensList(int category,int? brand, int? page, int? count, string article = "")
        {
            if (!string.IsNullOrEmpty(article))
            {
                ShowKitchenModel result = db.GetThing(article);
                if (result != null) return View("product", result);
            }

            if (page == null || page.Value == 0) page = 1;
            if (count == null || count.Value == 0) count = 10;

            var model = new ShowKitchensList(category, brand == null ? 0 : brand.Value, page.Value, count.Value);
            return View(model);
        }

        public virtual ActionResult ShowKitchensList(ShowKitchensList model)
        {
            db.ShowKitchens(ref model);
            return View(model);
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Страница контактов.";

            return View();
        }



        public virtual ActionResult About()
        {
            ContentModel model = db.GetContent("About");
            return View(model);
        }

        public virtual ActionResult ForDesigners()
        {
            ContentModel model = db.GetContent("ForDesigners");
            return View(model);
        }

        public virtual ActionResult EditPage(string Alias)
        {
            ContentModel model = db.GetContent(Alias);
            model.Alias = Alias;
            return View(model);
        }
        [HttpPost]
        //[Authorize(Roles = "admin,news")]
        public virtual ActionResult EditPage(Models.ContentModel model)
        {
            if (model.Date == new DateTime()) model.Date = DateTime.Now;
            model.Brief = "";
            model.IsPublished = true;
            model.Title = "";
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
    }
}
