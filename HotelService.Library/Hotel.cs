using HotelService.Library.Exceptions;

namespace HotelService.Library;

public class Hotel
{
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public string Description { get; private set; }
    
    //Composition
    private readonly List<ReservationRequest> _reservationRequests;

    public Hotel(string name, int capacity, string description)
    {
        ValidateConstructorInputs(name, capacity, description);
        Name = name;
        Capacity = capacity;
        Description = description;
        _reservationRequests = new List<ReservationRequest>();
    }

    private static void ValidateConstructorInputs(string name, int capacity, string description)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Hotel name cannot be null or empty.");
        if(capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero");
        if(string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
    }

    public void AddReservation(ReservationRequest reservationRequest)
    {
        if(reservationRequest == null)
            throw new ArgumentNullException(nameof(reservationRequest), "Reservation request cannot be null.");
        
        ValidateRoom(reservationRequest);
        _reservationRequests.Add(reservationRequest);
    }

    private void ValidateRoom(ReservationRequest reservationRequest)
    {
        if(_reservationRequests.Any(request => 
            request.Room == reservationRequest.Room && 
            reservationRequest.StartDate < request.EndDate && request.StartDate < reservationRequest.EndDate))
        {
            throw new AlreadyReservedException(reservationRequest.Room);
        }
        
        if (reservationRequest.Room > Capacity)
            throw new RoomCapacityExceededException(reservationRequest.Room, Capacity);
    }
}