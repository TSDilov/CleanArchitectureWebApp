using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWebApp.Application.Common.Exceptions;
using CleanArchitectureWebApp.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitectureWebApp.Application.TodoLists.Commands.UpdateTodoList;
using CleanArchitectureWebApp.Domain.Entities;
using FluentAssertions;

namespace CleanTesting.Application.IntegrationTests.TodoLists.Commands;
using static Testing;
public class UpdateTodoListTests : BaseTestFixture
{
    [Test]
    public void ShouldRequireValidTodoListId()
    {
        var command = new UpdateTodoListCommand 
        {
            Id = 125,
            Title = "New Title",
        };

        FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "First list",
        });

        await SendAsync(new CreateTodoListCommand
        {
            Title = "Second list",
        });

        var command = new UpdateTodoListCommand
        {
            Id = listId,
            Title = "Second list",
        };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title")))
                .And.Errors["Title"].Should().Contain("The specified title already exists.");
    }


    [Test]
    public async Task ShouldUpdateTodoLists()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "First list",
        });

        var command = new UpdateTodoListCommand
        {
            Id = listId,
            Title = "Updated list",
        };

        await SendAsync(command);

        var list = await FindAsync<TodoList>(listId);

        list!.Title.Should().Be(command.Title);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be(userId);
        list.LastModified.Should().NotBeNull();
        list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
