﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using pegabicho.backoffice.Services.Interfaces;
using System;
using System.IO;

namespace pegabicho.backoffice.Services {
    public class ViewRender : IViewRender {
        private IRazorViewEngine viewEngine;
        private ITempDataProvider tempDataProvider;
        private IServiceProvider serviceProvider;

        public ViewRender(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider) {
            this.viewEngine = viewEngine;
            this.tempDataProvider = tempDataProvider;
            this.serviceProvider = serviceProvider;
        }

        public string Render<TModel>(string name, TModel model) {
            var actionContext = GetActionContext();
            var viewEngineResult = viewEngine.FindView(actionContext, name, false);

            if (!viewEngineResult.Success)
                throw new InvalidOperationException(string.Format("Couldn't find view '{0}'", name));

            var view = viewEngineResult.View;

            using (var output = new StringWriter()) {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary()) {
                        Model = model
                    },
                    new TempDataDictionary(
                        actionContext.HttpContext,
                        tempDataProvider),
                    output,
                    new HtmlHelperOptions());

                view.RenderAsync(viewContext).GetAwaiter().GetResult();

                return output.ToString();
            }
        }

        private ActionContext GetActionContext() {
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = serviceProvider;
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
    }
}
