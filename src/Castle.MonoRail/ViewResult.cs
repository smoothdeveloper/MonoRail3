﻿namespace Castle.MonoRail
{
    using System;
    using System.Web;
    using Hosting.Mvc;

    public class ViewResult : ActionResult
    {
        private readonly string _viewName;

        public ViewResult(string viewName)
        {
            _viewName = viewName;
        }

        public override void Execute(ActionResultContext context, IMonoRailServices services)
        {
            var viewEngines = services.ViewEngines;

            var result = viewEngines.ResolveView(_viewName, null, new ViewResolutionContext(context));

            if (result.Successful)
            {
                try
                {
                    result.View.Process(
                        new ViewContext(
                            new HttpContextWrapper(HttpContext.Current), HttpContext.Current.Response.Output), 
                        HttpContext.Current.Response.Output);
                }
                finally
                {
                    result.ViewEngine.Release(result.View);
                }
            }
        }
    }
}
