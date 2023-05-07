using CleanArchitectureWebApp.Application.Common.Exceptions;
using CleanArchitectureWebApp.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitectureWebApp.Domain.Entities;
using FluentAssertions;

namespace CleanTesting.Application.IntegrationTests.TodoLists.Commands;
using static Testing;
public class CreateTodoListTests : BaseTestFixture
{
    [Test]
    public void ShouldRequireMinimumFields() 
    {
        var command = new CreateTodoListCommand();

        FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateTodoListCommand
        {
            Title = "New List",
        });

        var command = new CreateTodoListCommand
        {
            Title = "New List",
        };

        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoLists()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateTodoListCommand
        {
            Title = "New List",
        };

        var listId = await SendAsync(command);

        var list = await FindAsync<TodoList>(listId);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
