using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Models
{
    public class Manager 
    {
        public ZamerModels Zamer { get; set; }

        //public int UserId { get; set; }
        //public string UserName { get; set; }
        //public string Phone { get; set; }
        //public string Address { get; set; }
        //public string Time { get; set; }

        public RaschetModels Raschet { get; set; }

        //public int Type { get; set; }
        //public int Form { get; set; }
        //public string Wishes { get; set; }

        public DateTime Date { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public int Status { get; set; }
        public string Comments { get; set; }
        public int Id { get; set; }

        public Manager(ZamerModels zamer,RaschetModels raschet, DateTime date, int managerId, string managerName, int status, string comments, int id)
        {
            Raschet = raschet;
            Zamer = zamer;
            Date = date;
            ManagerId = managerId;
            ManagerName = managerName;
            Status = status;
            Comments = comments;
            Id = id;
        }

        //public Manager(RaschetModels raschet, DateTime date, int managerId, int status, string comments)
        //{
        //    Raschet = raschet;
        //    Date = date;
        //    ManagerId = managerId;
        //    Status = status;
        //    Comments = comments;
        //}

        public Manager()
        {
        }
    }
}
