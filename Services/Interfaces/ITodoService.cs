using Todo_App.DTOs;
using Todo_App.DTOs.Common;

namespace Todo_App.Services.Interfaces;

public interface ITodoService
{
    Task<ServiceResult<IEnumerable<TodoItemDto>>> GetAllAsync();
    Task<ServiceResult<TodoItemDto>> GetByIdAsync(int id);

    Task<ServiceResult> InsertAsync(TodoItemDto item);
    Task<ServiceResult> UpdateAsync(TodoItemDto item);
    Task<ServiceResult>DeleteAsync(int id);
}
