namespace HotelService.Library.Exceptions;

public class RoomCapacityExceededException: Exception
{
    public RoomCapacityExceededException(int room, int capacity) 
        : base($"Room number {room} exceeds the hotel capacity of {capacity}") {}
    
    public RoomCapacityExceededException(string message): base(message) {}
    
    public RoomCapacityExceededException(string message, Exception innerException): base(message, innerException) {}
}