using seminario.Application.TodoLists.Queries.ExportTodos;

namespace seminario.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
