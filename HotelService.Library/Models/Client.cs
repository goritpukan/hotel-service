namespace HotelService.Library.Models;

public class Client : ICloneable
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";

    public Client(string firstName, string lastName)
    {
        ValidateInputs(firstName, lastName);
        FirstName = firstName;
        LastName = lastName;
    }

    private static void ValidateInputs(string firstName, string lastName)
    {
         if(string.IsNullOrWhiteSpace(firstName))
             throw new ArgumentNullException(nameof(firstName), "First name cannot be null or empty");
         
         if(string.IsNullOrWhiteSpace(lastName))
             throw new ArgumentNullException(nameof(lastName), "Last name cannot be null or empty");
    }

    public void Update(string firstName, string lastName)
    {
        ValidateInputs(firstName, lastName);
        FirstName = firstName;
        LastName = lastName;
    }

    public object Clone()
    {
        return new Client(FirstName, LastName);
    }
}