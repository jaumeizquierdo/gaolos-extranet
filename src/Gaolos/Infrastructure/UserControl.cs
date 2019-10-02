using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using Gaolos.Session;

namespace Infrastructure
{
    public class UserControl : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            sesionExtranet Ses = context.HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
            if (Ses == null) { context.Result = new RedirectToActionResult("Login", "Login", null); return; }
            if (Ses.NIC == null) { context.Result = new RedirectToActionResult("Login", "Login", null); return; }
            if (Ses.NIC=="") { context.Result = new RedirectToActionResult("Login", "Login", null); return; }
        }
    }



}
