using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class ContactModel : PageModel
    {
        public bool hasData = false;
        public string firstname = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            hasData = true;
            firstname = Request.Form["firstname"];

    }
    }
}
