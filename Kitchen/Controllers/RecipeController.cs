using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kitchen.Models;
using System.IO;

namespace Kitchen.Controllers
{
    public partial class RecipeController : Controller
    {

        //[Authorize(Roles = "admin")]
        public virtual ActionResult EditRecipe(int Id = 0)
        {
            var res = db.GetRecipe(Id);
            return View(res == null ? new RecipeModel() : res);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        [ValidateInput(false)]
        public virtual ActionResult EditRecipe(RecipeModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    model.Id = db.AddRecipe(model);
                }
                else
                    db.UpdateRecipe(model);
            }
            return RedirectToAction("EditRecipe", new { Id = model.Id });
        }

        [Authorize]
        //[HttpPost]
        public virtual ActionResult setPhoto(HttpPostedFileBase Photo,int id)
        {
            if (Photo != null && Photo.ContentLength > 0)
            {
                //получаем фотку
                byte[] buffer = null;
                using (var binaryReader = new BinaryReader(Photo.InputStream))
                {
                    buffer = binaryReader.ReadBytes(Photo.ContentLength);
                }

                //сохраняем в базу
                db.setPhoto(id, buffer);
            }

            return RedirectToAction("EditRecipe/"+id);
        }


        public virtual ActionResult GetRecipe(int id)
        {
            return View(db.GetRecipe(id));
        }

        public virtual ActionResult RecipeBook()
        {
            return View(db.GetRecipeList());
        }
    }
}
