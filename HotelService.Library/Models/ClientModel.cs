namespace HotelService.Library.Models;

public class ClientModel
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    
    private readonly List<ReservationModel> _reservationRequests;

    public ClientModel(string firstName, string lastName)
    {
        ValidateConstructorInputs(firstName, lastName);
        FirstName = firstName;
        LastName = lastName;
        _reservationRequests = new List<ReservationModel>();
    }

    private static void ValidateConstructorInputs(string firstName, string lastName)
    {
         if(string.IsNullOrWhiteSpace(firstName))
             throw new ArgumentNullException(nameof(firstName), "First name cannot be null or empty");
         
         if(string.IsNullOrWhiteSpace(lastName))
             throw new ArgumentNullException(nameof(lastName), "Last name cannot be null or empty");
    }

    public void AddReservationRequest(ReservationModel reservationModel)
    {
        if(reservationModel == null)
            throw new ArgumentNullException(nameof(reservationModel), "Reservation request cannot be null");
        _reservationRequests.Add(reservationModel);
    }
    
    
}