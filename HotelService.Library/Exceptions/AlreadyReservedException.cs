namespace HotelService.Library.Exceptions;

public class AlreadyReservedException : Exception
{
    public AlreadyReservedException(int room): base($"Room number: {room} is already reserved"){}
    
    public AlreadyReservedException(string message) : base(message){}
    
    public AlreadyReservedException(string message, Exception innerException) : base(message, innerException){}
}