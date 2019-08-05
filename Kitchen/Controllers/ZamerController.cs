using Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace Kitchen.Controllers
{
    public partial class ZamerController : Controller
    {
        [HttpGet]
        [Authorize]
        public virtual ActionResult Index()
        {
            return View(db.GetInfo(Convert.ToInt32(User.Identity.Name)));
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult SendZakaz(ZamerModels model)
        {
            SendMail(model);
            model.UserId = int.Parse(User.Identity.Name);
            ViewData["result"] = db.SetZakaz(model);
            return View();
        }


        public static void SendMail(ZamerModels model)
        {
            string message = "<div><table style='border-collapse: collapse;'><tr><td style='width:100px;border:3px solid gray'>Имя:  </td><td style='border:3px solid gray'>" + model.UserName + "</td></tr><tr><td style='width:100px;border:3px solid gray'>Телефон: </td><td style='border:3px solid gray'>" + model.Phone + "</td></tr><tr><td style='width:100px;border:3px solid gray'>Адрес: </td><td style='border:3px solid gray'>" + model.Address + "</td></tr><tr><td style='width:100px;border:3px solid gray'>Удобное время: </td><td style='border:3px solid gray'>" + model.Time + "</table></div>";  
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("phoenix_net@bk.ru"); //info@rls.center
                mail.To.Add(new MailAddress("info@graf-msk.ru"));
                mail.Subject = "Онлайн заявка от " + DateTime.Now.ToLongTimeString();

                mail.Body = message;

                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.bk.ru";
                client.Port = 2525;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("phoenix_net@bk.ru", "moneyfornothing");//info@rls.center ghbdtn123
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
                //MailMessage mail = new MailMessage();
                //mail.From = new MailAddress("phoenix_net@bk.ru"); //info@rls.center
                //mail.To.Add(new MailAddress("info@graf-msk.ru"));
                //mail.Subject = "Онлайн заявка от " + DateTime.Now.ToLongDateString();
                //mail.Body = message;
                //SmtpClient client = new SmtpClient();
                //client.Host = "smtp.bk.ru";
                //client.Port = 25;
                //client.EnableSsl = true;
                //client.Credentials = new NetworkCredential("phoenix_net@bk.ru", "moneyfornothing");//info@rls.center ghbdtn123
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.Send(mail);
                //mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }
    }
}
