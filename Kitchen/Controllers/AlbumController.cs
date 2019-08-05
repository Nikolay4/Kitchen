using Kitchen.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Controllers
{
    public partial class AlbumController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }


        [MvcSiteMapNode(Title = "Альбом", ParentKey = "Home")]
        public virtual ActionResult ShowAlbum(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index");
            return View(db.showAlbum(id, true));
        }

        public virtual ActionResult AddAlbum(string id)
        {
            return View(db.showAlbum(id, false));
        }

        [HttpPost]
        //[Authorize(Roles = "admin, news")]
        public virtual ActionResult AddAlbum(AlbumsModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.AlbumId == 0)
                {
                    //model.site = Enums.getSite(Request.Url.Host);
                    model.Position = model.Position ?? 0;
                    model.AlbumId = db.addAlbum(model);
                }
                else
                {
                    db.editAlbum(model);
                }

                return RedirectToAction("ShowAlbum", new { id = model.Alias });
            }
            return View();
        }

        public virtual ActionResult AddPhoto(int AlbumId)
        {
            return View(new AddPhotoModel(AlbumId));
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult AddPhoto(AddPhotoModel model)
        {
            const int maxWidth = 840;
            //int PhotoId = 0;
            //byte[] resPhoto = null;

            if (model.Photo != null && model.Photo.ContentLength > 0)
            {
                //получаем фотку
                Bitmap src = Image.FromStream(model.Photo.InputStream) as Bitmap;

                //обрезаем и уменьшаем размер для превьюхи
                Rectangle cropRect = new Rectangle(model.x, model.y, model.w, model.h);
                Bitmap bmpCrop = src.Clone(cropRect, src.PixelFormat);

                Bitmap bmpCropPreview = new Bitmap(120, 90);
                using (Graphics g = Graphics.FromImage(bmpCropPreview))
                    g.DrawImage(bmpCrop, 0, 0, 120, 90);

                //если слишком здорова картинка, то уменьшаем по ширине
                if (src.Width > maxWidth)
                {
                    var ratio = (double)src.Width / maxWidth;
                    bmpCrop = new Bitmap(840, (int)(src.Height / ratio));
                    using (Graphics g = Graphics.FromImage(bmpCrop))
                        g.DrawImage(src, 0, 0, 840, (int)(src.Height / ratio));
                    src = bmpCrop;
                }
                //Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                //using (Graphics g = Graphics.FromImage(target))
                //{
                //    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                //                     cropRect,
                //                     GraphicsUnit.Pixel);
                //}


                ImageConverter converter = new ImageConverter();
                if (db.addPhoto(model.AlbumId, (byte[])converter.ConvertTo(bmpCropPreview, typeof(byte[])), (byte[])converter.ConvertTo(src, typeof(byte[])), model.Description, Convert.ToInt32(User.Identity.Name)))
                {
                    return RedirectToAction("ShowAlbum", new { id = model.AlbumId });
                }
                else
                {
                    ViewData["Error"] = "Похоже, что не записалось :(";
                }
                //resPhoto = (byte[])converter.ConvertTo(bmpCropPreview, typeof(byte[]));

                //byte[] buffer = null;
                //using (var binaryReader = new BinaryReader(model.Photo.InputStream))
                //{
                //    buffer = binaryReader.ReadBytes(model.Photo.ContentLength);

                //}

                //сохраняем в базу
                //db.addImg(model.ThingId, buffer);
            }

            //ViewBag.Photo = resPhoto;
            return View(model);
            //return RedirectToAction("EditPhoto", new { PhotoId = PhotoId });
        }

        public virtual ActionResult DeletePhoto(int PhotoId)
        {
            if (User == null || !User.Identity.IsAuthenticated) return RedirectToAction("Photo", PhotoId);


            var album = db.showAlbumForPhoto(PhotoId);

            if (db.IsInRole(User.Identity.Name,"admin") /*|| User.IsInRole("news")*/)
            {
                db.deletePhoto(PhotoId);
            }
            else
            {
                if (!string.IsNullOrEmpty(album.PermissionRole))
                {
                    //if (User.IsInRole(album.PermissionRole)) 
                        db.deletePhoto(PhotoId);
                }
            }

            return RedirectToAction(MVC.Album.ShowAlbum(album.Alias));
        }

        public virtual ActionResult EditPhoto(int PhotoId)
        {
            //byte[] res = db.getPhoto(PhotoId);
            //ViewBag.Photo = res;
            return View();
        }

        [MvcSiteMapNode(Title = "Фото", ParentKey = "Home")]
        public virtual ActionResult Photo(int id, int count, int number)
        {
            ViewBag.count = count;
            ViewBag.number = number;
            return View(db.showPhoto(id, User==null || User.Identity.Name == "" ? 0 : Convert.ToInt32(User.Identity.Name)));
        }

        public bool PhotoVoting(int PhotoId, decimal val)
        {
            if (val >= 1 && val <= 5)
            return db.SetVote(PhotoId, User == null || User.Identity.Name == "" ? 0 : Convert.ToInt32(User.Identity.Name), val);
            return false;
            
        }
    }
}
