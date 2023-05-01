using CleanArchitectureWebApp.Application.Common.Mappings;
using CleanArchitectureWebApp.Domain.Entities;

namespace CleanArchitectureWebApp.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}
