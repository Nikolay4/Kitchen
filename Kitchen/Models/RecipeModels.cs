using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage=" Требуется поле.")]
        public string Name { get; set; }

        [Required(ErrorMessage=" Требуется поле.")]
        public string Ingridients { get; set; }

        [Required(ErrorMessage=" Требуется поле.")]
        public string Brief { get; set; }

        [Required(ErrorMessage=" Требуется поле.")]
        public string Description { get; set; }

        //[Required(ErrorMessage=" Требуется поле.")]
        public byte[] Photo { get; set; }

        public RecipeModel(int id, string name, string ingridients, string brief, string description, byte[] photo)
        {
            Id = id;
            Name = name;
            Ingridients = ingridients;
            Brief = brief;
            Description = description;
            Photo = photo;
        }

        public RecipeModel()
        {
            Id = 0;
        }
    }
}
