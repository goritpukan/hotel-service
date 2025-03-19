using HotelService.Library.Exceptions;

namespace HotelService.Library;

public class ReservationRequest
{ 
    public int Room { get; private set;}
    public string Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public Hotel Hotel { get; private set; }

    public ReservationRequest(int room, string description, DateTime startDate, DateTime endDate, Hotel hotel)
    {
        ValidateConstructorInputs(room, description, startDate, endDate, hotel);
        
        Room = room;
        Description = description;
        EndDate = endDate;
        StartDate = startDate;
        Hotel = hotel;
    }

    private static void ValidateConstructorInputs(int room, string description, DateTime startDate, DateTime endDate, Hotel hotel)
    {
        if (room <= 0) 
            throw new ArgumentOutOfRangeException(nameof(room), "Room number must be greater than zero");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "Description cannot be null");
        
        if (startDate < DateTime.Now) 
            throw new InvalidReservationDataException("Start date cannot be earlier than now");
        
        if (endDate < startDate)
            throw new InvalidReservationDataException("End date cannot be earlier than start date");
        
        if(hotel == null) 
            throw new ArgumentNullException(nameof(hotel), "Hotel cannot be null");
    }
}