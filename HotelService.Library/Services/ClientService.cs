using HotelService.Library.Exceptions;
using HotelService.Library.Models;
using HotelService.Library.Utilities.Comparers;

namespace HotelService.Library.Services;

public class ClientService
{
    private readonly List<ClientModel> _clients = new List<ClientModel>();

    public void AddClient(ClientModel client)
    {
        if(client is null)
            throw new ArgumentNullException(nameof(client), "Client cannot be null");
        _clients.Add(client);
    }

    private ClientModel FindClientByFullName(string fullName)
    {
        if(string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentNullException(nameof(fullName), "Full name cannot be null");
        
        var client =  _clients.FirstOrDefault(client => client.FullName == fullName);
        if(client is null)
            throw new ClientNotFoundException(fullName);
        
        return client;
    }

    public void DeleteClientByFullName(string fullName)
    {
       var client = FindClientByFullName(fullName);
        _clients.Remove(client);
    }

    public void ChangeClientByFullName(string fullName, ClientModel client)
    {
        if(string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentNullException(nameof(fullName), "Full name cannot be null");
        
        if(client is null)
            throw new ArgumentNullException(nameof(client), "Client cannot be null");
        
        int index = _clients.FindIndex(client => client.FullName == fullName);
        if(index == -1)
            throw new ClientNotFoundException(fullName);
        _clients[index] = client;
    }

    public ClientModel GetClientByFullName(string fullName)
    {
       var client = FindClientByFullName(fullName);
        return (ClientModel)client.Clone();
    }

    public List<ClientModel> GetAllClients()
    {
        return _clients.Select(client => (ClientModel)client.Clone()).ToList();
    }

    public void SortClientsByFirstName()
    {
        _clients.Sort(new ClientFisrtNameComparer());
    }

    public void SortClientsByLastName()
    {
        _clients.Sort(new ClientLastNameComparer());
    }
}