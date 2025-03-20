using HotelService.Library.Exceptions;

namespace HotelService.Library.Models;

public class ReservationModel
{ 
    public int Room { get; private set;}
    public string Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public HotelModel HotelModel { get; private set; }

    public ReservationModel(int room, string description, DateTime startDate, DateTime endDate, HotelModel hotelModel)
    {
        ValidateConstructorInputs(room, description, startDate, endDate, hotelModel);
        
        Room = room;
        Description = description;
        EndDate = endDate;
        StartDate = startDate;
        HotelModel = hotelModel;
    }

    private static void ValidateConstructorInputs(int room, string description, DateTime startDate, DateTime endDate, HotelModel hotelModel)
    {
        if (room <= 0) 
            throw new ArgumentOutOfRangeException(nameof(room), "Room number must be greater than zero");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "Description cannot be null");
        
        if (startDate < DateTime.Now) 
            throw new InvalidReservationDataException("Start date cannot be earlier than now");
        
        if (endDate < startDate)
            throw new InvalidReservationDataException("End date cannot be earlier than start date");
        
        if(hotelModel == null) 
            throw new ArgumentNullException(nameof(hotelModel), "Hotel cannot be null");
    }
}