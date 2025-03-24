using HotelService.Library.Exceptions;

namespace HotelService.Library.Models;

public class ReservationModel : ICloneable
{ 
    public int Room { get; private set;}
    public string Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public HotelModel Hotel { get; private set; }
    public ClientModel Client { get; private set; }

    public ReservationModel(int room, string description, DateTime startDate, DateTime endDate, HotelModel hotel, ClientModel client)
    {
        ValidateConstructorInputs(room, description, startDate, endDate, hotel, client);
        
        Room = room;
        Description = description;
        EndDate = endDate;
        StartDate = startDate;
        Hotel = hotel;
        Client = client;
    }

    private static void ValidateConstructorInputs(int room, string description, DateTime startDate, DateTime endDate, HotelModel hotel, ClientModel client)
    {
        if (room <= 0) 
            throw new ArgumentOutOfRangeException(nameof(room), "Room number must be greater than zero");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "Description cannot be null");
        
        if (startDate < DateTime.Now) 
            throw new InvalidReservationDataException("Start date cannot be earlier than now");
        
        if (endDate < startDate)
            throw new InvalidReservationDataException("End date cannot be earlier than start date");
        
        if(hotel is null) 
            throw new ArgumentNullException(nameof(hotel), "Hotel cannot be null");
        
        if(client is null)
            throw new ArgumentNullException(nameof(client), "Client cannot be null");
    }

    public object Clone()
    {
        return new ReservationModel(Room, Description, StartDate, EndDate, (HotelModel)Hotel.Clone(), (ClientModel)Client.Clone());
    }
}