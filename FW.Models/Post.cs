using System.Web.Mvc;

namespace FW.Models
{
    public class Post : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string ThumbnailImageFilePath { get; set; }
    }
}
