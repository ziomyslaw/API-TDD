using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using NSubstitute;

namespace WebApplicationTdd.UnitTests.Utilities;
public static class ControllerBaseExtensions
{
    public static void SetupContext(this ControllerBase controller, Action<HttpContext>? httpContextAction = null, HttpRequest? request = null)
    {
        var httpContext = Substitute.For<HttpContext>();
        if (httpContextAction is not null) httpContextAction(httpContext);
        if (request is not null) httpContext.Request.Returns(request);

        controller.ControllerContext = new ControllerContext(new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor()));
    }
}