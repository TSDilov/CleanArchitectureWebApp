using CleanArchitectureWebApp.Application.TodoLists.Queries.ExportTodos;

namespace CleanArchitectureWebApp.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
