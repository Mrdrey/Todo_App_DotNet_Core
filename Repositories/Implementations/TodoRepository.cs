using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using Todo_App.Models;
using Todo_App.Services.Interfaces;

namespace Todo_App.Services.Implementations
{
   
    public class TodoRepository : ITodoRepository
    {
        private readonly IDbConnection _connection;

        public TodoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task Delete(int id)
        {

            var query = "DELETE FROM Todos WHERE Id = @id";
            await _connection.QueryAsync(query, new { Id = id });
         
        }

        public async Task<IEnumerable<TodoItem>> GetAll()

        {
            
                var query = "SELECT * FROM Todos;";
                var result = await _connection.QueryAsync<TodoItem>(query);
                return result;
            

        }
        public async Task<TodoItem> GetById(int id)
        {
            var query = "SELECT * FROM Todos WHERE Id = @id;";
            var result = await _connection.QuerySingleOrDefaultAsync<TodoItem>(query, new { Id = id });
            return result;
        }

        public async Task Insert(TodoItem item)
        {
           
                var query = "INSERT INTO Todos(Title, IsCompleted)" +
                    "VALUES(@Title, @IsCompleted)";
                await _connection.QueryAsync(query, item);
            
        }

        public async Task Update(TodoItem item)
        {
            
            var query = "UPDATE Todos " +
                "SET Title = @title, IsCompleted = @isCompleted";
            await _connection.QueryAsync(query, item);
        }
    }
}
