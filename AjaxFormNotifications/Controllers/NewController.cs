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

            //Response.StatusCode = 404;
            return PartialView("_ErrorNotification", $"Failed to save value {data.Value}");
        }
    }

    public class Data
    {
        public int Value { get; set; }
    }
}
