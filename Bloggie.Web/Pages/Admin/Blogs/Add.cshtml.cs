using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        // property binding - bind a single property
        //[BindProperty]
        //public string Heading { get; set; }

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }

        // use constructor injection to inject the DbContext
        public AddModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            // Read Incoming reuqest data from the form
            //var heading = Request.Form["heading"];
            //var pageTitle = Request.Form["pageTitle"];
            //var content = Request.Form["content"];
            //var shortDescription = Request.Form["shortDescription"];

            //map from addblogpostrequest model to blogpost model
            var blogPost = new BlogPost()
            {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                Content = AddBlogPostRequest.Content,
                ShortDescription = AddBlogPostRequest.ShortDescription,
                FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                UrlHandle = AddBlogPostRequest.UrlHandle,
                PublishedDate = AddBlogPostRequest.PublishedDate,
                Author = AddBlogPostRequest.Author,
                Visible = AddBlogPostRequest.Visible
            };

            // use DbContext to save the data to the database
            //await bloggieDbContext.BlogPosts.AddAsync(blogPost);
            //await bloggieDbContext.SaveChangesAsync();

            // update code to use add async method
            await blogPostRepository.AddAsync(blogPost);

            // Create Notifications using temp Data
            TempData["MessageDescription"] = "New Blog Post Added Successfully";

            return RedirectToPage("/Admin/Blogs/List");
        }
    }
}
