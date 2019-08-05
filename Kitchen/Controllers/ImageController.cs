using Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Kitchen.Controllers
{
    public partial class ImageController : Controller
    {
        //передаем id записи в таблице
        public virtual ActionResult GetImage(int id)
        {
            //byte[] imgByteArr;
            ////что то там делаем...
            /////в результате заполняем массив байтов imgByteArr
            //// 
            //Image img = null;
            //using (var ms = new MemoryStream())
            //{
            //    using (var sw = new StreamWriter(ms))
            //    {
            //        sw.Write(imgByteArr);
            //        img = Image.FromStream(ms);
            //    }
            //}

            DataTable dt = db.GetData("select * from Photo where id=" + id);
            //Image image = Image.FromStream(new MemoryStream(byte[] buffer));
            byte[] data = (byte[])dt.Rows[0]["Image"];
            //MemoryStream mem = new MemoryStream(data);
            //Bitmap bmp = new Bitmap(30, 30);

            //bmp.Save(Response.OutputStream, ImageFormat.Png);
            //-----Response.BinaryWrite(data);


            //Response.Clear();
            //--Response.ContentType = "image/png";
            ////Response.Write(Image.FromStream(mem));
            //Response.Write(Image.FromStream(mem));
            return View(data);
        }

        //передаем id записи вещи
        public virtual ActionResult GetImagesFromThing(int id)
        {
            DataTable dt = db.GetData("select * from Photo where id=" + id);
            List<images> result = new List<images>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    result.Add(new images((int)dRow["id"], (int)dRow["Id"], (byte[])dRow["Image"]));
                }
            }
            //byte[] data = (byte[])dt.Rows[0]["Image"];

            //result.Add(data);
            return View(result);
        }

        public virtual ActionResult EditImagesFromThing(int id = 0)
        {
            if (id == 0) return View();


            DataTable dt = db.GetData("select * from Photo where albumId=" + id);
            List<images> result = new List<images>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    result.Add(new images((int)dRow["id"], (int)dRow["albumId"], (byte[])dRow["image"]));
                }
            }
            //byte[] data = (byte[])dt.Rows[0]["Image"];

            //result.Add(data);
            return View(result);
        }


        public virtual ActionResult DeleteImg(int img_Id, int kitchen_Id)
        {
            if (img_Id == 0) img_Id = 3;
            db.SetData("delete from Photo where id=" + img_Id);
            return RedirectToAction("Edit", "Home", new { id = kitchen_Id });
        }

        [HttpPost]
        public virtual ActionResult AddImg(addImg model)
        {
            if (model.AddImage != null && model.AddImage.ContentLength > 0)
            {
                //получаем фотку
                byte[] buffer = null;
                using (var binaryReader = new BinaryReader(model.AddImage.InputStream))
                {
                    buffer = binaryReader.ReadBytes(model.AddImage.ContentLength);
                }

                //сохраняем в базу
                db.addImg(model.Id, buffer);
            }
            return RedirectToAction(MVC.Home.Edit(model.Id));
        }

        public struct images
        {
            public int Id;
            public int Kitchen_Id;
            public byte[] Image;

            public images(int id, int kitchen_Id, byte[] img)
            {
                Id = id;
                Kitchen_Id = kitchen_Id;
                Image = img;
            }
        }

        public virtual ActionResult ImagesForNews(int Id)
        {
            DataTable dt = db.GetData("select * from Photo where id=" + Id);
            byte[] data = (byte[])dt.Rows[0]["Image"];
            //MemoryStream ms = new MemoryStream(data);
            //return Image.FromStream(ms);
            return File(data, "image/jpg");
            //return View(data);
        }
    }
}
