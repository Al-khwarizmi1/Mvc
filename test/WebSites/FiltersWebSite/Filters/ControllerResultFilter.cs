// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace FiltersWebSite
{
    public class ControllerResultFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ActionDescriptor.DisplayName == "FiltersWebSite.ProductsController.GetPrice")
            {
                context.HttpContext.Response.Headers.Append("filters",
                    "On Controller Result Filter - OnResultExecuting");
            }
            else
            {
                context.Result = Helpers.GetContentResult(context.Result, "Controller Result filter");
            }
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.ActionDescriptor.DisplayName == "FiltersWebSite.ProductsController.GetPrice")
            {
                context.HttpContext.Response.Headers.Append("filters",
                    "On Controller Result Filter - OnResultExecuted");
            }
        }
    }
}