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
        public async void OnActionExecutionAsync_Authorize_True()
        {
            ///Arrange
            const string KEY = "TestKey";
            const string NAME = "name";
            const string INVALID = "invalid";
            const int RESULT = 200;

            var mockRepo = new Mock<ILogger<APIKeyAuthAttribute>>();
            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            var mockConfig = new Mock<IConfiguration>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            APIKeyAuthAttribute tskController = new APIKeyAuthAttribute(mockRepo.Object);

            modelState.AddModelError(NAME, INVALID);
            httpContext.Request.Headers[ConstantStrings.APIKEY] = KEY;
            mockConfig.Setup(c => c.GetSection(It.IsAny<String>()).Value).Returns(KEY);
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
            actionExecutingContext.Result = new StatusCodeResult(RESULT);
            var mockAEDelegate = new Mock<ActionExecutionDelegate>();

            ///Act 
            await tskController.OnActionExecutionAsync(actionExecutingContext, mockAEDelegate.Object);

            ///Assert
            Assert.Equal(RESULT, ((StatusCodeResult)actionExecutingContext.Result).StatusCode);
        }

        /// <summary>
        /// Negative flow for authentication. Wrong Key in Header
        /// </summary>
        [Fact]
        public async void OnActionExecutionAsync_Authorize_False_WrongKey()
        {
            ///Arrange
            const string KEY = "TestKey";
            const string WRONG_KEY = "TestKey1";
            const string NAME = "name";
            const string INVALID = "invalid";
            const int RESULT = 200;
            const int ERROR_RESULT = 401;

            var mockRepo = new Mock<ILogger<APIKeyAuthAttribute>>();
            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            var mockConfig = new Mock<IConfiguration>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            APIKeyAuthAttribute tskController = new APIKeyAuthAttribute(mockRepo.Object);
            modelState.AddModelError(NAME, INVALID);
            httpContext.Request.Headers[ConstantStrings.APIKEY] = WRONG_KEY;
            mockConfig.Setup(c => c.GetSection(It.IsAny<String>()).Value).Returns(KEY);
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
            actionExecutingContext.Result = new StatusCodeResult(RESULT);
            var mockAEDelegate = new Mock<ActionExecutionDelegate>();

            ///Act 
            await tskController.OnActionExecutionAsync(actionExecutingContext, mockAEDelegate.Object);

            ///Assert
            Assert.Equal(ERROR_RESULT, ((StatusCodeResult)actionExecutingContext.Result).StatusCode);
        }

        /// <summary>
        /// Negative flow for authentication. Null Key in Header
        /// </summary>
        [Fact]
        public async void OnActionExecutionAsync_Authorize_False_NullKey()
        {
            ///Arrange
            const string NAME = "name";
            const string INVALID = "invalid";
            const string KEY = "TestKey";
            const int ERROR_RESULT = 401;

            var mockRepo = new Mock<ILogger<APIKeyAuthAttribute>>();
            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            var mockConfig = new Mock<IConfiguration>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            APIKeyAuthAttribute tskController = new APIKeyAuthAttribute(mockRepo.Object);

            modelState.AddModelError(NAME, INVALID);
            httpContext.Request.Headers[ConstantStrings.APIKEY] = KEY;
            mockConfig.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);
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

            ///Act 
            await tskController.OnActionExecutionAsync(actionExecutingContext, mockAEDelegate.Object);

            ///Assert
            Assert.Equal(ERROR_RESULT, ((StatusCodeResult)actionExecutingContext.Result).StatusCode);
        }

        /// <summary>
        /// Exception flow. Null Service provider
        /// </summary>
        [Fact]
        public async void OnActionExecutionAsync_Authorize_ThorwException()
        {
            ///Arrange
            const string NAME = "name";
            const string INVALID = "invalid";
            const string KEY = "TestKey";

            var mockRepo = new Mock<ILogger<APIKeyAuthAttribute>>();
            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            var mockConfig = new Mock<IConfiguration>();
            var mockCSec = new Mock<IConfigurationSection>();

            APIKeyAuthAttribute tskController = new APIKeyAuthAttribute(mockRepo.Object);
            modelState.AddModelError(NAME, INVALID);
            httpContext.Request.Headers[ConstantStrings.APIKEY] = KEY;
            mockCSec.Object[ConstantStrings.APIKEY] = KEY;
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

            try
            {
                ///Act 
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

