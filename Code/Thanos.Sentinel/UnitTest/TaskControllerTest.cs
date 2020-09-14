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
    public class TaskControllerTest
    {
        [Fact]
        public void TaskController_Get_NotNull()
        {
            ///Setup
            var mockRepo = new Mock<ILogger<TaskController>>();
            TaskController tskController = new TaskController(mockRepo.Object);

            ///Test 
            var result = tskController.Get();

            ///Assert
            Assert.NotNull(result);
        }

    }
}
