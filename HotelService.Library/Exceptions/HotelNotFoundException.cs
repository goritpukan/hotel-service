namespace HotelService.Library.Exceptions;

//Inheritance
public class HotelNotFoundException : NotFoundException
{ 
    public HotelNotFoundException() : base("Hotel not found"){}
    
    public HotelNotFoundException(string hotelName) : base($"Hotel {hotelName} not found"){}
}