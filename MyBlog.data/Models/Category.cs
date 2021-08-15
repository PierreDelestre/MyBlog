using MyBlog.data.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.data.Models
{
    public class Category : IMyBlogItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
