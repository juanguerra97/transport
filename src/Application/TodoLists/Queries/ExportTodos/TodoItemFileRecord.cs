using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
