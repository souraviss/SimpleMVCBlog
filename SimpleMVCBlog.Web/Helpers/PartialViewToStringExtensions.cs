using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SimpleMVCBlog.Web.Helpers
{
    /// <summary>Fake IView implementation, only used to instantiate an HtmlHelper.</summary> 
    public class FakeView : IView
    {
        #region IView Members
        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public static class MvcPartialHelper
    {

        public static string RenderPartialToString(this HtmlHelper html, string viewName, object viewData)
        {
            return RenderViewToString(html.ViewContext.Controller.ControllerContext, viewName, viewData);
        }
        /// <summary>Renders a view to string.</summary> 
        public static string RenderViewToString(this Controller controller, string viewName, object viewData)
        {
            var httpContextBase = new HttpContextWrapper(HttpContext.Current);
            RouteData routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            controller.ControllerContext = new ControllerContext(new RequestContext(httpContextBase, routeData), controller);
            return RenderViewToString(controller.ControllerContext, viewName, viewData);
        }

        private static string RenderViewToString(ControllerContext context,
                                                string viewName, object viewData)
        {
            //Create memory writer 
            var sb = new StringBuilder();
            var memWriter = new StringWriter(sb);

            //Create fake http context to render the view 
            var fakeResponse = new HttpResponse(memWriter);
            var fakeContext = new HttpContext(HttpContext.Current.Request, fakeResponse);
            RouteData routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            var fakeControllerContext = new ControllerContext(new HttpContextWrapper(HttpContext.Current), routeData, context.Controller);

            var oldContext = HttpContext.Current;
            HttpContext.Current = fakeContext;
            var view = ViewEngines.Engines.FindPartialView(context, "~/Views/GenericContent/Index.cshtml").View;
            //Use HtmlHelper to render partial view to fake context 
            var html = new HtmlHelper(new ViewContext(context, view, new ViewDataDictionary(), new TempDataDictionary(), TextWriter.Null), new ViewPage());
            html.RenderPartial(viewName, viewData);

            //Restore context 
            HttpContext.Current = oldContext;

            //Flush memory and return output 
            memWriter.Flush();
            return sb.ToString();
        }


        public static string RenderPartial<TController>(string partialViewName, object model, TController controller)
            where TController : Controller
        {
            //var httpContextBase = new HttpContextWrapper(HttpContext.Current);
            ////var routeData = new RouteData();

            ////routeData.Values.Add("controller", typeof(TController).Name);

            //RouteData routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            //var controllerContext = new ControllerContext(new RequestContext(httpContextBase, routeData), controller);
            //controllerContext.Controller.ControllerContext = new ControllerContext(new RequestContext(httpContextBase, routeData), controller);
            //var view = ViewEngines.Engines.FindPartialView(controllerContext, "~/Views/GenericContent/" + partialViewName + ".cshtml").View;// ViewEngines.Engines.FindPartialView(controllerContext, partialViewName).View;
            //var viewContext = new ViewContext(controllerContext,
            //                                  view,
            //                                  new ViewDataDictionary { Model = model },
            //                                  new TempDataDictionary(),
            //                                  httpContextBase.Response.Output);

            //view.Render(viewContext, httpContextBase.Response.Output);



            try
            {
                //using (StringWriter sw = new StringWriter())
                //{


                //    var httpContextBase = new HttpContextWrapper(HttpContext.Current);
                //    RouteData routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
                //    var factory = new ControllerFactory();;
                //    var context = new RequestContext(httpContextBase, routeData);
                //    Type type = factory.GetController(context, "GenericController");


                //        IController controller1 = factory.GetController(context, type);

                //    controller.ActionInvoker.

                //    var controllerContext = new ControllerContext(new RequestContext(httpContextBase, routeData), controller);
                //    controllerContext.Controller.ControllerContext = new ControllerContext(new RequestContext(httpContextBase, routeData), controller);
                //    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, "~/Views/GenericContent/" + partialViewName + ".cshtml");
                //    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                //    viewResult.View.Render(viewContext, sw);

                //    return sw.GetStringBuilder().ToString();
                //}
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public static void RenderPartial<TController>(string action, object model, TController controller, RouteData routeData)
            where TController : Controller
        {
            var httpContextBase = new HttpContextWrapper(HttpContext.Current);
            var controllerContext = new ControllerContext(new RequestContext(httpContextBase, routeData), controller);
            controllerContext.Controller.ControllerContext = new ControllerContext(new RequestContext(httpContextBase, routeData), controller);
            ControllerActionInvoker invoker = new ControllerActionInvoker();
            invoker.InvokeAction(controllerContext, action);
        }
    }
}