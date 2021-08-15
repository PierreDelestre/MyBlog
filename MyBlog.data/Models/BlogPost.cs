using MyBlog.data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.data.Models
{
    public class BlogPost : IMyBlogItem
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        public String Text { get; set; }
        public DateTime PublishDate { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
