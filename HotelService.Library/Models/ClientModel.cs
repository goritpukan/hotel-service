namespace HotelService.Library.Models;

public class ClientModel : ICloneable
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";

    public ClientModel(string firstName, string lastName)
    {
        ValidateConstructorInputs(firstName, lastName);
        FirstName = firstName;
        LastName = lastName;
    }

    private static void ValidateConstructorInputs(string firstName, string lastName)
    {
         if(string.IsNullOrWhiteSpace(firstName))
             throw new ArgumentNullException(nameof(firstName), "First name cannot be null or empty");
         
         if(string.IsNullOrWhiteSpace(lastName))
             throw new ArgumentNullException(nameof(lastName), "Last name cannot be null or empty");
    }

    public object Clone()
    {
        return new ClientModel(FirstName, LastName);
    }
}