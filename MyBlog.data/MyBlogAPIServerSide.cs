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

        
        private async Task DeleteItem(IMyBlogItem item)
        {
            using var context = factory.CreateDbContext();
            context.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBlogPost(BlogPost item)
        {
            await DeleteItem(item);
        }

        public async Task DeleteCategory(Category item)
        {
            await DeleteItem(item);
        }

        public async Task DeleteTag(Tag item)
        {
            await DeleteItem(item);
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

        public async Task<List<Category>> GetCategories()
        {
            using var context = factory.CreateDbContext();
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            using var context = factory.CreateDbContext();
            return await context.Categories.Include(p => p.BlogPosts).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Tag> GetTag(int id)
        {
            using var context = factory.CreateDbContext();
            return await context.Tags.Include(t => t.BlogPosts).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Tag>> getTags()
        {
            using var context = factory.CreateDbContext();
            return await context.Tags.ToListAsync();
        }

        private async Task<IMyBlogItem> SaveItem(IMyBlogItem item)
        {
            using var context = factory.CreateDbContext();
            if (item.Id == 0)
            {
                context.Add(item);
            }
            else
            {
                if (item is BlogPost)
                {
                    var post = item as BlogPost;
                    
                    var currentPost = await context.BlogPosts.Include(b => b.Category).Include(b => b.Tags).FirstOrDefaultAsync(b => b.Id == post.Id);
                    currentPost.PublishDate = post.PublishDate;
                    currentPost.Title = post.Title;
                    currentPost.Text = post.Text;

                    var tagsIds = post.Tags.Select(t => t.Id);
                    currentPost.Tags = context.Tags.Where(t => tagsIds.Contains(t.Id)).ToList();
                    currentPost.Category = await context.Categories.FirstOrDefaultAsync(c => c.Id == post.Category.Id);
                }
                else
                {
                    context.Entry(item).State = EntityState.Modified;
                }
            }

            await context.SaveChangesAsync();
            return item;
        }

        public async Task<BlogPost> SaveBlogPost(BlogPost item)
        {
            return (await SaveItem(item)) as BlogPost;
        }

        public async Task<Category> SaveCategory(Category item)
        {
            return (await SaveItem(item)) as Category;
        }

        public async Task<Tag> SaveTag(Tag item)
        {
            return (await SaveItem(item)) as Tag;
        }
    }
}
