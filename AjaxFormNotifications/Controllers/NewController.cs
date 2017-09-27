using System.IO;
using System.Web.Mvc;

namespace AjaxFormNotifications.Controllers
{
    public class NewController : Controller
    {
        [HttpGet]
        public ActionResult ShowForm()
        {
            return View(new Data());
        }

        [HttpPost]
        public PartialViewResult SaveForm(Data data)
        {
            var even = (data.Value % 2) == 0;

            if (even)
            {
                Response.StatusCode = 200;
                return PartialView("_SuccessNotification", $"Saved value {data.Value}");
            }

            Response.StatusCode = 404;
            return PartialView("_ErrorNotification", $"Failed to save value {data.Value}");
        }

        [HttpGet]
        public ActionResult ShowJsonForm()
        {
            return View(new Data());
        }

        [HttpPost]
        public JsonResult SaveJsonForm(Data data)
        {
            var even = (data.Value % 2) == 0;

            if (even)
            {
                Response.StatusCode = 200;
                return Json(new
                {
                    Message = $"Saved {data.Value}!",
                    ViewHtml = this.RenderViewToString("_SuccessNotification", $"Saved value {data.Value}")
                });
            }

            Response.StatusCode = 500;
            return Json(new
            {
                Message = $"Could not save {data.Value}!",
                ViewHtml = this.RenderViewToString("_ErrorNotification", $"Could not save value {data.Value}")
            });
        }
    }

    public class Data
    {
        public int Value { get; set; }
    }

    public static class Extensions
    {
        public static string RenderViewToString(this Controller controller, string viewName, object model)
        {
            using (var writer = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                controller.ViewData.Model = model;
                var viewCxt = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, writer);
                viewCxt.View.Render(viewCxt, writer);
                return writer.ToString();
            }
        }
    }
}
