using CleanArchitectureWebApp.Application.TodoLists.Queries.GetTodos;
using CleanArchitectureWebApp.Domain.Entities;
using FluentAssertions;

namespace CleanTesting.Application.IntegrationTests.TodoLists.Queries;

using static Testing;
public class GetTodosTests
{
    [Test]
    public async Task ShouldReturnAllListsAndAssociatedItems()
    {
        // Arrange
        await AddAsync(new TodoList
        {
            Title = "Shopping",
            Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" }
                    }
        });

        var query = new GetTodosQuery();

        // Act
        TodosVm result = await SendAsync(query);


        // Assert
        result.Should().NotBeNull();
        result.Lists.Should().HaveCount(1);
        result.Lists.First().Items.Should().HaveCount(6);
    }
}
