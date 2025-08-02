using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.Data.SqlClient;
using System.Data;
using Todo_App.DTOs;
using Todo_App.DTOs.Common;
using Todo_App.Models;
using Todo_App.Services.Interfaces;

namespace Todo_App.Services.Implementations;

public class TodoService : ITodoService
{   
    private readonly ITodoRepository _repository;
    private readonly ILogger _logger;

    public TodoService(ITodoRepository repository, ILogger logger = null)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger;
    }

    //done
    public async Task<ServiceResult> DeleteAsync(int id)
    {
        try
        {
            var todo = await _repository.GetById(id);



            if (todo == null)
                return new ServiceResult<TodoItemDto>
                {
                    IsSuccess = true,
                    Message = "Deletion of todo was unsuccessful."
                };
                

            await _repository.Delete(id);
            return new ServiceResult<TodoItemDto>
            {
                IsSuccess = true,
                Message = $" Todo '{todo?.Title}' successfully deleted.",
                
                
            };
            
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Failed to Delete Object");
            return new ServiceResult<TodoItemDto>
            {
                IsSuccess = false,
                Message = $"{ex.Message}"
                
            };
        }
       
    }
    
    //done
    public async Task<ServiceResult<IEnumerable<TodoItemDto>>> GetAllAsync()
    {
        try
        {
         
            var todos = await _repository.GetAll();
            var todoDtos = todos.Select(todo => new TodoItemDto
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted
            });
            return new ServiceResult<IEnumerable<TodoItemDto>>
            {
                IsSuccess = true,
                Message = "Get All Successfully.",
                Data = todoDtos
            };
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Failed GetAll");
            return new ServiceResult<IEnumerable<TodoItemDto>> 
            {
                IsSuccess = false,
                Message = ex.Message
                
            };
        }

    }
    //done
    public async  Task<ServiceResult<TodoItemDto>> GetByIdAsync(int id)
    {
        try
        {
            if (id == 0)
            {
                return new ServiceResult<TodoItemDto>
                {
                    IsSuccess = false,
                    Message = $"Failed to Get {id} Todo"
                };
            }
            var todo = await _repository.GetById(id);

            return new ServiceResult<TodoItemDto>
            {
                IsSuccess = true,
                Message = "Success"
            };

        }
        catch(Exception ex)
        {
            return new ServiceResult<TodoItemDto>
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }


    }
    //done
    public async  Task<ServiceResult> InsertAsync(TodoItemDto itemDto)
    {
        try
        {
           if(itemDto != null)
            {
                var todo = new TodoItem
                {
                    Title = itemDto.Title,
                    IsCompleted = itemDto.IsCompleted,
                };

                await _repository.Insert(todo);
                return new ServiceResult<TodoItemDto>
                {
                    IsSuccess = true,
                    Message = "Object inserted succesfully."
                };

            }
            return new ServiceResult<TodoItemDto>
            {
                IsSuccess = false,
                Message = "Failed to insert object."
            };
           


          

        }catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to insert object");
            return new ServiceResult<TodoItemDto>
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
        


    }
    //done
    public async Task<ServiceResult> UpdateAsync(TodoItemDto item)
    {
        try
        {
            if(item != null)
            {
                var todos = new TodoItem
                {
                    Title = item.Title,
                    IsCompleted = item.IsCompleted,
                };
                await _repository.Update(todos);
                return new ServiceResult<TodoItemDto>
                {
                    IsSuccess = true,
                    Message = $"{item.Title} Updated Successfully!"
                };
            }
            return new ServiceResult<TodoItemDto>
            {
                IsSuccess = false,
                Message = "Failed to Update."
            };

        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);

            return new ServiceResult<TodoItemDto>
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
}
