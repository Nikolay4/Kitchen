using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Models
{
    public class ZamerModels 
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Time { get; set; }

        public ZamerModels(int userId,string userName, string phone, string address)
        {
            UserId = userId;
            UserName = userName;
            Phone = phone;
            Address = address;
        }

        public ZamerModels(int userId, string userName, string phone, string address, string time)
        {
            UserId = userId;
            UserName = userName;
            Phone = phone;
            Address = address;
            Time = time;
        }
        public ZamerModels()
        {
        }

        public ZamerModels(int userId)
        {
            UserId = -1;
        }
    }
}
