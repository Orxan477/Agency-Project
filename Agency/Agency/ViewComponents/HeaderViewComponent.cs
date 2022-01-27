using Microsoft.AspNetCore.Mvc;

namespace Agency.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
