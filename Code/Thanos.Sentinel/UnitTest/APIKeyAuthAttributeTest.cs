using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Thanos.Sentinel.Controllers;
using Thanos.Sentinel.Filters;
using Thanos.Sentinel.Helpers;
using Xunit;

namespace Thanos.Sentinel.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class APIKeyAuthAttributeTest
    {
        [Fact]
        public async void APIKeyAuthAttribute_Get_Unauthorized()
        {
            ///Setup
            var mockRepo = new Mock<ILogger<APIKeyAuthAttribute>>();
            APIKeyAuthAttribute tskController = new APIKeyAuthAttribute(mockRepo.Object);
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers[ConstantStrings.APIKEY] = "TestKey";
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IConfiguration)))
                .Returns(mockConfig.Object);
            httpContext.RequestServices = serviceProviderMock.Object;
            var actionContext = new ActionContext(
                httpContext,
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );
            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            var mockAEDelegate = new Mock<ActionExecutionDelegate>();

            ///Test 
            await tskController.OnActionExecutionAsync(actionExecutingContext, mockAEDelegate.Object);

            ///Assert
            Assert.Equal(401, ((StatusCodeResult)actionExecutingContext.Result).StatusCode);
        }

        [Fact]
        public async void APIKeyAuthAttribute_Get_Exception()
        {
            ///Setup
            var mockRepo = new Mock<ILogger<APIKeyAuthAttribute>>();
            APIKeyAuthAttribute tskController = new APIKeyAuthAttribute(mockRepo.Object);
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers[ConstantStrings.APIKEY] = "TestKey";
            var mockConfig = new Mock<IConfiguration>();
            var mockCSec = new Mock<IConfigurationSection>();
            mockCSec.Object[ConstantStrings.APIKEY] = "TestKey";
            mockConfig.Setup(c => c.GetSection(It.IsAny<String>())).Returns(mockCSec.Object);

            var actionContext = new ActionContext(
                httpContext,
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );
            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            var mockAEDelegate = new Mock<ActionExecutionDelegate>();

            ///Test 
            try
            {
                await tskController.OnActionExecutionAsync(actionExecutingContext, mockAEDelegate.Object);
                Assert.False(true);
            }
            catch(Exception e)
            {
                ///Assert
                Assert.Equal(typeof(ArgumentNullException), e.GetType());
            }
        }
    }
}

