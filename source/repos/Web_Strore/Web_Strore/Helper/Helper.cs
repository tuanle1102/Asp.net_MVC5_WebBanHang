using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Strore.Helper
{
    public class Helper
    {
        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null) {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter()) {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult vieweResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                      controller.ControllerContext,
                      vieweResult.View,
                      controller.ViewData,
                      controller.TempData,
                      sw,
                      new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions()

                    );
                vieweResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();

            }
        }
    }
}
