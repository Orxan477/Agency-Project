using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.admin.ViewComponents
{
    public class SidebarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
