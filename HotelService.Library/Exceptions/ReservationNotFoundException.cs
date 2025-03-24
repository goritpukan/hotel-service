namespace HotelService.Library.Exceptions;

public class ReservationNotFoundException : NotFoundException
{
    public ReservationNotFoundException() : base("Reservation not found") { }
    
    public ReservationNotFoundException(string message) : base(message) { }
}