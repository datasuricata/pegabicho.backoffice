using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using pegabicho.shared.Notifications;

namespace pegabicho.backoffice.Filters {
    public class ExceptionFilter : ExceptionFilterAttribute {

        public override void OnException(ExceptionContext context) {
            if (FilterValidation.RunValidator(context))
                context.Result = FilterValidation.Filter(context);

            base.OnException(context);
        }
    }

    public static class FilterValidation {
        private static string exception;
        private static int statusCode;
        private static bool isAjaxCall;

        public static bool RunValidator(ExceptionContext context) {
            isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            statusCode = context.HttpContext.Response.StatusCode;

            if (!isAjaxCall) {
                if (statusCode == 200) {
                    context.HttpContext.Response.ContentType = "application/json";
                    exception = "Error: " + context.Exception.Message;
                    return true;
                } else {
                    return false;
                }
            } else {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = 500;
                exception = context.Exception is DomainNotifyer? "Error: " + context.Exception.Message : "Error: An error ocorred";
                exception = "Error: " + context.Exception.Message;
                return true;
            }
        }
        public static JsonResult Filter(ExceptionContext context) {
            context.ExceptionHandled = true;

            return new JsonResult(exception);
        }
    }
}
