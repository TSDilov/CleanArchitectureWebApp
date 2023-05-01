using CleanArchitectureWebApp.Application.Common.Mappings;
using CleanArchitectureWebApp.Domain.Entities;

namespace CleanArchitectureWebApp.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
