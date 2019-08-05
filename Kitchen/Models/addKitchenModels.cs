using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Models
{
    public class addKitchenModels 
    {
       public int? Id { get; set; }

        [Required]
        public Enums.category Category { get; set; }

        [Required]
        public Enums.brand Brand { get; set; }

        [Required(ErrorMessage=" Требуется поле.")]
        [StringLength(15, ErrorMessage = "Артикул не должен превышать 15 символов")]
        public string Article { get; set; }

        [StringLength(50, ErrorMessage = "Название не должно превышать 50 символов")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Описание не должно превышать 1000 символов")]
        public string Description { get; set; }

        public addKitchenModels(int id, Enums.category category, Enums.brand brand ,string article, string name, string description)
        {
            this.Id = id;
            Category = category;
            Brand = brand;
            Article = article;
            Name = name;
            Description = description;
        }

        public addKitchenModels()
        {
            Id = 0;
        }
    }

    public class ShowKitchenModel
    {
        public int Id { get; set; }
        public Enums.category Category { get; set; }
        public Enums.brand Brand { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<byte[]> Images { get; set; }

        public ShowKitchenModel(int id, Enums.category category, Enums.brand brand, string article, string name, string description)
        {
            Id = id;
            Category = category;
            Brand = brand;
            Article = article;
            Name = name;
            Description = description;
            Images = new List<byte[]>();
        }
    }

   
    public class addImg
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public HttpPostedFileBase AddImage { get; set; }
    }

    public class ShowKitchensList
    {
        public int category { get; set; }

        public int brand { get; set; }

        public int page { get; set; }

        public int count { get; set; }

        public int countThings { get; set; }

        public List<strListThigs> listThing;

        public struct strListThigs
        {
            public string Name;
            public int ThingId;
            public string Article;
            public byte[] Img;

            public strListThigs(string name, int thingId, string article, byte[] img)
            {
                Name = name;
                ThingId = thingId;
                Article = article;
                Img = img;
            }
        }

        public ShowKitchensList(int category, int brand, int page, int count)
        {
            this.brand = brand;
            this.category = category;
            this.page = page;
            this.count = count;
        }
    }
}
