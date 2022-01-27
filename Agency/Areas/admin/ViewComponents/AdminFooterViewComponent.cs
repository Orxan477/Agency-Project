using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.admin.ViewComponents
{
    public class AdminFooterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
