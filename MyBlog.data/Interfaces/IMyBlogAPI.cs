using MyBlog.data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.data.Interfaces
{
    public  interface IMyBlogAPI
    {
        Task<int> GetBlogPostCount();
        Task<List<BlogPost>> GetBlogPosts(int numberOfPosts, int startIndex);
        Task<List<Category>> GetCategories();
        Task<List<Tag>> getTags();


        Task<BlogPost> GetBlogPost(int id);
        Task<Category> GetCategory(int id);
        Task<Tag> GetTag(int id);


        Task<BlogPost> SaveBlogPost(BlogPost item);
        Task<Category> SaveCategory(Category item);
        Task<Tag> SaveTag(Tag item);


        Task DeleteBlogPost(BlogPost item);
        Task DeleteCategory(Category item);
        Task DeleteTag(Tag item);
    }
}
