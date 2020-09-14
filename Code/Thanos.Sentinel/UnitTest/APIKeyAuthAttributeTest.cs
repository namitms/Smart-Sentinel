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
using Thanos.Sentinel.Filters;
using Thanos.Sentinel.Helpers;
using Xunit;

namespace Thanos.Sentinel.UnitTest
{
    /// <summary>
    /// APIKeyAuthenticationAttribute test project
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class APIKeyAuthAttributeTest
    {
        /// <summary>
        /// Positive flow for authentication. Matching key in the header and config
        /// </summary>
        [Fact]
        public async void APIKeyAuthAttribute_Get_Authorized()
        {
            ///Setup
            const string key = "TestKey";
            var mockRepo = new Mock<ILogger<APIKeyAuthAttribute>>();
            APIKeyAuthAttribute tskController = new APIKeyAuthAttribute(mockRepo.Object);
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers[ConstantStrings.APIKEY] = key;
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c.GetSection(It.IsAny<String>()).Value).Returns(key);
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

            //Set default result as success. Will remain the same unless unauthorized
            actionExecutingContext.Result = new StatusCodeResult(200);
            var mockAEDelegate = new Mock<ActionExecutionDelegate>();

            ///Test 
            await tskController.OnActionExecutionAsync(actionExecutingContext, mockAEDelegate.Object);

            ///Assert
            Assert.Equal(200, ((StatusCodeResult)actionExecutingContext.Result).StatusCode);
        }

        /// <summary>
        /// Negative flow for authentication. Wrong Key in Header
        /// </summary>
        [Fact]
        public async void APIKeyAuthAttribute_Get_Unauthorized_Wrong_Key()
        {
            ///Setup
            const string key = "TestKey";
            const string wrongKey = "TestKey1";
            var mockRepo = new Mock<ILogger<APIKeyAuthAttribute>>();
            APIKeyAuthAttribute tskController = new APIKeyAuthAttribute(mockRepo.Object);
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers[ConstantStrings.APIKEY] = wrongKey;
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c.GetSection(It.IsAny<String>()).Value).Returns(key);
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

            //Set default result as success. Will remain the same unless unauthorized
            actionExecutingContext.Result = new StatusCodeResult(200);
            var mockAEDelegate = new Mock<ActionExecutionDelegate>();

            ///Test 
            await tskController.OnActionExecutionAsync(actionExecutingContext, mockAEDelegate.Object);

            ///Assert
            Assert.Equal(401, ((StatusCodeResult)actionExecutingContext.Result).StatusCode);
        }

        /// <summary>
        /// Negative flow for authentication. Null Key in Header
        /// </summary>
        [Fact]
        public async void APIKeyAuthAttribute_Get_Unauthorized_Null_Key()
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

        /// <summary>
        /// Exception flow. Null Service provider
        /// </summary>
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
            catch (Exception e)
            {
                ///Assert
                Assert.Equal(typeof(ArgumentNullException), e.GetType());
            }
        }
    }
}

