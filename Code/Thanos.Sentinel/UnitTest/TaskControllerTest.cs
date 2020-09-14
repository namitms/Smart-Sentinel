using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Thanos.Sentinel.Controllers;
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
