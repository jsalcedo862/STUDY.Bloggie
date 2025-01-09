using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        [BindProperty]
        public BlogPost BlogPost { get; set; }

        public EditModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task OnGet(Guid id)
        {
            BlogPost = await blogPostRepository.GetAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            await blogPostRepository.UpdateAsync(BlogPost);

            // Create Notifications using View Data
            //ViewData["MessageDescription"] = "Blog Post Updated Successfully";

            // Create Notifications using enum
            ViewData["Notification"] = new Notification
            {
                Message = "Blog Post Updated Successfully",
                Type = Enums.NotificationType.Success
            };

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await blogPostRepository.DeleteAsync(BlogPost.Id);
            if (deleted)
            {
                return RedirectToPage("/Admin/Blogs/List");
            }
                
            return Page();
        }
    }
}
