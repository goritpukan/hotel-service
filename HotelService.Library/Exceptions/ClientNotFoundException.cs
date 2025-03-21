namespace HotelService.Library.Exceptions;

public class ClientNotFoundException : NotFoundException
{
    public ClientNotFoundException() : base("Client not found"){}
    
    public ClientNotFoundException(string fullName) : base($"Client: {fullName} not found"){}
}