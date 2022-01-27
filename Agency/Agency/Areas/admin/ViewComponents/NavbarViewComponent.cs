using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.admin.ViewComponents
{
    public class NavbarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
