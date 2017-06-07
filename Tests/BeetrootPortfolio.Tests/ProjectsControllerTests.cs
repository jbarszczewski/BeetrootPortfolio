using BeetrootPortfolio.Configuration;
using BeetrootPortfolio.Controllers;
using BeetrootPortfolio.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BeetrootPortfolio.Tests
{
    public class ProjectsControllerTests
    {
        [Fact]
        public async Task GetInfo_ReturnsNotFound_WhenWrongKey()
        {
            // Arrange
            var mockRepository = new Mock<IProjectsRepository>();
            var mockSettings = new Mock<IOptions<PortfolioSettings>>();
            ProjectsController controller = new ProjectsController(mockRepository.Object, mockSettings.Object);

            // Act
            var result = controller.GetInfo("wrongKey");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
