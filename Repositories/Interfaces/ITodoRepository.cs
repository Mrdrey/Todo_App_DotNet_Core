using Todo_App.Models;

namespace Todo_App.Services.Interfaces
{
    public interface ITodoRepository
    {
       Task<IEnumerable<TodoItem>> GetAll();
       Task<TodoItem> GetById(int id);
        Task Insert(TodoItem item);
       Task Update(TodoItem item);
        Task Delete(int id);



    }
}
