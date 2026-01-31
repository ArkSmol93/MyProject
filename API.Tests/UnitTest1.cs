using API.Controllers;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ResourcesControllerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateResource_AddsResource()
    {
        // Arrange
        var context = GetDbContext();
        var controller = new ResourcesController(context);
        var resource = new Resource { Name = "Test", Description = "Test desc" };

        // Act
        var result = await controller.CreateResource(resource);

        // Assert
        var resourcesInDb = await context.Resources.ToListAsync();
        Assert.Single(resourcesInDb);
        Assert.Equal("Test", resourcesInDb[0].Name);
    }
}
