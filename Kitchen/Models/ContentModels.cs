using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Models
{

    public class ContentModel
    {

        public int? id { get; set; }

        [Required(ErrorMessage = " Требуется поле alias")]
        [StringLength(50, ErrorMessage = "Alias не должен превышать 50 символов")]
        public string Alias { get; set; }

        [Required(ErrorMessage = " Требуется поле Заголовок")]
        public string Title { get; set; }

        public DateTime? Date { get; set; }

        public bool IsPublished { get; set; }

        //public string Author { get; set; }

        public string Brief { get; set; }

        [Required]
        public string Description { get; set; }

        //[StringLength(100, ErrorMessage = "Тэги не должен превышать 1000 символов")]
        //public string tags { get; set; }

        //public Enums.TypeContent TypeContent { get; set; }

        //public int menuWisdom { get; set; }

        //public int menuBORW { get; set; }

        //public Enums.Sites Site { get; set; }

        public ContentModel(int id, string alias, string title, DateTime date, bool isPublished, string brief, string descr)
        {
            this.id = id;
            Alias = alias;
            Title = title;
            Date = date;
            IsPublished = isPublished;
            Brief = brief;
            Description = descr;
        }

        public ContentModel()
        {
            Date = DateTime.Now;
        }

    }

    public class ListNews
    {
        public List<ContentModel> news;
        public int count { get; set; }
        public int page { get; set; }

        public ListNews(List<ContentModel> News, int Count, int Page)
        {
            news = News;
            count = Count;
            page = Page;
        }

        public ListNews()
        {

        }
    }

}
