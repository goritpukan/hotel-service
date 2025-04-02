namespace HotelService.Library.Exceptions;

public class RoomNotFoundException: Exception
{
    public RoomNotFoundException(int room) 
        : base($"Room number {room} is not found") {}
    
    public RoomNotFoundException(string message): base(message) {}
    
    public RoomNotFoundException(string message, Exception innerException): base(message, innerException) {}
}