using HotelService.Library.Exceptions;

namespace HotelService.Library.Models;

public class HotelModel : ICloneable
{
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public string Description { get; private set; }
    //Composition

    public HotelModel(string name, int capacity, string description)
    {
        ValidateConstructorInputs(name, capacity, description);
        Name = name;
        Capacity = capacity;
        Description = description;
    }

    private static void ValidateConstructorInputs(string name, int capacity, string description)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Hotel name cannot be null or empty");
        if(capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero");
        if(string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "Description cannot be null or empty");
    }

    public object Clone()
    {
        return new HotelModel(Name, Capacity, Description);
    }

    /*public void AddReservation(int room, string description, DateTime startDate, DateTime endDate, ClientModel clientModel)
    {
        var reservation = new ReservationModel(room, description, startDate, endDate, this);
        ValidateRoom(reservation);
        clientModel.AddReservationRequest(reservation);
        _reservationRequests.Add(reservation);
    }

    private void ValidateRoom(ReservationModel reservationModel)
    {
        if(_reservationRequests.Any(request => 
            request.Room == reservationModel.Room && 
            reservationModel.StartDate < request.EndDate && request.StartDate < reservationModel.EndDate))
        {
            throw new AlreadyReservedException(reservationModel.Room);
        }
        
        if (reservationModel.Room > Capacity)
            throw new RoomCapacityExceededException(reservationModel.Room, Capacity);
    }
    */
}