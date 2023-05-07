using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWebApp.Application.Common.Exceptions;
using CleanArchitectureWebApp.Application.TodoLists.Commands.CreateTodoList;
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
}
