using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Components
{
    public class NavbarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
            return View();
        }
    }
}
