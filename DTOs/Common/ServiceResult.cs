namespace Todo_App.DTOs.Common;

public class ServiceResult
{
    public bool IsSuccess { get; set; } 
    public string? Message { get; set; }
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }
}