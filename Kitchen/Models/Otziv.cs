using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Models
{
    public class OtzivList
    {
        public int page { get; set; }

        public int count { get; set; }

        public int countOtziv { get; set; }

        public List<Otziv> otziv;

        public struct Otziv
        {
            public int Id;
            public string UserName;  
            public DateTime Date; 
            public string Description;
            public bool IsShowed;     

            public Otziv(int id, string userName, DateTime date, string description, bool isShowed)
            {
                Id = id;
                UserName = userName;
                Date = date;
                Description = description;
                IsShowed = isShowed;
            }
        }

        public OtzivList(int page, int count)
        {
            this.page = page;
            this.count = count;
        }
    }
}
