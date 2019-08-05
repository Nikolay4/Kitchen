using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kitchen.Models
{
    public class AlbumsModel
    {
        public int AlbumId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Alias cannot be longer than 50 characters.")]
        public string Alias { get; set; }

        public int? Position { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        //public Enums.Sites site { get; set; }
        public byte[] ImgAlbum { get; set; }
        public string PermissionRole { get; set; }

        public List<EditPhotoModel> PreviewPhotos;

        public AlbumsModel(int id, string alias, int pos, string name, string descr, byte[] img, string permission)
        {
            AlbumId = id;
            Alias = alias;
            Position = pos;
            Name = name;
            Description = descr;
            ImgAlbum = img;
            PermissionRole = permission;
        }
        public AlbumsModel()
        {

        }
    }

    public class AddPhotoModel
    {
        [Required]
        public int AlbumId { get; set; }

        [Required]
        public HttpPostedFileBase Photo { get; set; }

        public string Description { get; set; }

        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }

        public AddPhotoModel()
        {
        }

        public AddPhotoModel(int album)
        {
            AlbumId = album;
        }
    }

    public class EditPhotoModel
    {
        [Required]
        public int PhotoId { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        public EditPhotoModel(int id, byte[] img)
        {
            PhotoId = id;
            Photo = img;
        }
        public EditPhotoModel()
        {
        }
    }

    public class PhotoModel
    {
        public int PhotoId { get; set; }
        public int AlbumId { get; set; }
        public byte[] Img { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public int RatingCount { get; set; }
        public bool IsCanVote { get; set; }

        public PhotoModel(int id, int albumId, byte[] img, DateTime date, string descr, decimal rating, int ratingCount, bool isCanVote)
        {
            PhotoId = id;
            AlbumId = albumId;
            Img = img;
            Date = date;
            Description = descr;
            Rating = rating;
            RatingCount = ratingCount;
            IsCanVote = isCanVote;
        }
    }
}