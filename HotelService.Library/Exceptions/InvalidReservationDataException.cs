namespace HotelService.Library.Exceptions;

public class InvalidReservationDataException: Exception
{
    public InvalidReservationDataException() : base("Invalid reservation data"){}
    public InvalidReservationDataException(string message) : base(message){}
    
    public InvalidReservationDataException(string message, Exception innerException) : base(message, innerException){}
}