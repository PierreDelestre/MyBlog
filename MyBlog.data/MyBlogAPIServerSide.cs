using Microsoft.EntityFrameworkCore;
using MyBlog.data.Interfaces;
using MyBlog.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.data
{
    public class MyBlogAPIServerSide : IMyBlogAPI
    {
        IDbContextFactory<MyBlogDBContext> factory;
        public MyBlogAPIServerSide(IDbContextFactory<MyBlogDBContext> factory)
        {
            this.factory = factory;
        }

        public Task DeleteBlogPost(BlogPost item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategory(Category item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTag(Tag item)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost> GetBlogPost(int id)
        {
            using var context = factory.CreateDbContext();
            return await context.BlogPosts.Include(p => p.Category).Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> GetBlogPostCount()
        {
            using var context = factory.CreateDbContext();
            return await context.BlogPosts.CountAsync();
        }

        public async Task<List<BlogPost>> GetBlogPosts(int numberOfPosts, int startIndex)
        {
            using var context = factory.CreateDbContext();
            return await context.BlogPosts.OrderByDescending(p => p.PublishDate).Skip(startIndex).Take(numberOfPosts).ToListAsync();
        }

        public Task<List<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetTag(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tag>> getTags()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost> SaveBlogPost(BlogPost item)
        {
            throw new NotImplementedException();
        }

        public Task<Category> SaveCategory(Category item)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> SaveTag(Tag item)
        {
            throw new NotImplementedException();
        }
    }
}
