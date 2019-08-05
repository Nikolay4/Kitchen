using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Models
{
    public class RaschetModels
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Enums.category Type { get; set; }
        public int Form { get; set; }
        public string Text { get; set; }

        public RaschetModels(int userId, string userName,Enums.category type, int form,string text)
        {
            UserId = userId;
            UserName = userName;
            Type = type;
            Form = form;
            Text = text;
        }
        public RaschetModels()
        {
        }

        public RaschetModels(int userId)
        {
            UserId = -1;
        }
    }
}
