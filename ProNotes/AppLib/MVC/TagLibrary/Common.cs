using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ProNotes.AppLib.MVC.TagLibrary
{
    public static class Common
    {
        private static IActionContextAccessor? _actionContextAccessor;

        public static void TagLibConfiguration(IActionContextAccessor actionContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
        }

        public static string GetControllerName()
        {
            if (_actionContextAccessor?.ActionContext is ControllerContext)
            {
                ControllerContext? controllerContext = _actionContextAccessor?.ActionContext as ControllerContext;
                return controllerContext?.ActionDescriptor.ControllerName ?? string.Empty;
            }

            return string.Empty;
        }

        public static string GetActionName()
        {
            if (_actionContextAccessor?.ActionContext is ControllerContext)
            {
                ControllerContext? controllerContext = _actionContextAccessor?.ActionContext as ControllerContext;
                return controllerContext?.ActionDescriptor.ActionName ?? string.Empty;
            }

            return string.Empty;
        }
    }
}