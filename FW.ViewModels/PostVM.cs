using Microsoft.AspNetCore.Http;
using System.Web.Mvc;

namespace FW.ViewModels
{
    public class PostVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public bool? IsEnable { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public IFormFile Thumbnail { get; set; }
        public string IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }
}
