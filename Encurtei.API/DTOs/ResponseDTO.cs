namespace Encurtei.API.DTOs;

public class ResponseDTO<T>
{
    public T? Data { get; set; }
    public object? Error { get; set; }

    public ResponseDTO(T? data, object? error)
    {
        Data = data;
        Error = error;
    }
}
