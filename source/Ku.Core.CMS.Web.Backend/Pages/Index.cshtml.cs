using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ku.Core.CMS.Web.Backend.Pages
{
    [Auth]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["LoginUserName"] = User.GetUserNameOrNull();
        }
    }
}