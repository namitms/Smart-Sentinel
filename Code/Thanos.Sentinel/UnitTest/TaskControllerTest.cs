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
        public void Get_GetTasks_NotNull()
        {
            ///Arrange
            var mockRepo = new Mock<ILogger<TaskController>>();
            TaskController tskController = new TaskController(mockRepo.Object);

            ///Act 
            var result = tskController.Get();

            ///Assert
            Assert.NotNull(result);
        }

    }
}
