namespace HotelService.Library;

public class Client
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    
    private readonly List<ReservationRequest> _reservationRequests;

    public Client(string firstName, string lastName)
    {
        ValidateConstructorInputs(firstName, lastName);
        FirstName = firstName;
        LastName = lastName;
        _reservationRequests = new List<ReservationRequest>();
    }

    private static void ValidateConstructorInputs(string fisrtName, string lastName)
    {
         if(string.IsNullOrWhiteSpace(fisrtName))
             throw new ArgumentNullException(nameof(fisrtName), "First name cannot be null or empty");
         
         if(string.IsNullOrWhiteSpace(lastName))
             throw new ArgumentNullException(nameof(lastName), "Last name cannot be null or empty");
    }

    public void AddReservationRequest(ReservationRequest reservationRequest)
    {
        if(reservationRequest == null)
            throw new ArgumentNullException(nameof(reservationRequest), "Reservation request cannot be null");
        _reservationRequests.Add(reservationRequest);
    }
    
    
}