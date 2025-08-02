using Microsoft.AspNetCore.Mvc;
using Todo_App.DTOs;
using Todo_App.Models;
using Todo_App.Services.Implementations;
using Todo_App.Services.Interfaces;

namespace Todo_App.Controllers;
public class TodoController : Controller
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAllTodos()
    {
        var items = await _todoService.GetAllAsync();

       return Ok(items);
    }
    [HttpGet]
    public IActionResult AddTodo()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTodo(TodoItemDto itemDto)
    {
      

        if (itemDto == null)
        {
            return BadRequest();
        }

        if(!ModelState.IsValid)
        {
           return BadRequest(ModelState);
        }
        var items = await _todoService.InsertAsync(itemDto);
        return CreatedAtAction(nameof(GetAllTodos), items, new {Id = itemDto.Id});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditTodo(TodoItemDto todoItemDto)
    {
        if (todoItemDto == null)
        {
            return BadRequest();
        }

        var item = await _todoService.UpdateAsync(todoItemDto);

        if (item == null)
        {
            return NotFound(); // or return BadRequest("Update failed.");
        }
        return Ok(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        if(id == 0)
        {
            return BadRequest(id);
        }
        if(ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        await _todoService.DeleteAsync(id);
        return Ok(id);
    }
}
