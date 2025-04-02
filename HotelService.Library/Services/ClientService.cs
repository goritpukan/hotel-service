using HotelService.Library.Exceptions;
using HotelService.Library.Interfaces;
using HotelService.Library.Models;
using HotelService.Library.Utilities.Comparers;

namespace HotelService.Library.Services;

public class ClientService : IClientService
{
    private readonly List<Client> _clients = new List<Client>();

    /// <summary>
    /// Adds a new client to the system.
    /// </summary>
    /// <param name="client">The client to be added.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="client"/> is null.
    /// </exception>
    public void AddClient(Client client)
    {
        if(client is null)
            throw new ArgumentNullException(nameof(client), "Client cannot be null");
        _clients.Add(client);
    }

    /// <summary>
    /// Finds a client by their full name.
    /// </summary>
    /// <param name="fullName">The full name of the client.</param>
    /// <returns>The matching client.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="fullName"/> is null or empty.
    /// </exception>
    /// <exception cref="ClientNotFoundException">
    /// Thrown when no client with the given full name is found.
    /// </exception>
    private Client FindClientByFullName(string fullName)
    {
        if(string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentNullException(nameof(fullName), "Full name cannot be null");
        
        var client =  _clients.FirstOrDefault(client => client.FullName == fullName);
        if(client is null)
            throw new ClientNotFoundException(fullName);
        
        return client;
    }

    /// <summary>
    /// Deletes a client by their full name.
    /// </summary>
    /// <param name="fullName">The full name of the client to delete.</param>
    /// <exception cref="ClientNotFoundException">
    /// Thrown when no client with the given full name is found.
    /// </exception>
    public void DeleteClientByFullName(string fullName)
    {
       var client = FindClientByFullName(fullName);
        _clients.Remove(client);
    }

    /// <summary>
    /// Updates the first and last name of a client found by their full name.
    /// </summary>
    /// <param name="fullName">The current full name of the client.</param>
    /// <param name="newFirstName">The new first name to update.</param>
    /// <param name="newLastName">The new last name to update.</param>
    /// <exception cref="ClientNotFoundException">
    /// Thrown when no client with the given full name is found.
    /// </exception>
    public void ChangeClientByFullName(string fullName, string newFirstName, string newLastName)
    {
        var client = FindClientByFullName(fullName);
        client.Update(newFirstName, newLastName);
    }
    
    /// <summary>
    /// Retrieves a client by their full name.
    /// </summary>
    /// <param name="fullName">The full name of the client.</param>
    /// <returns>A cloned instance of the client.</returns>
    /// <exception cref="ClientNotFoundException">
    /// Thrown when no client with the given full name is found.
    /// </exception>
    public Client GetClientByFullName(string fullName)
    {
       var client = FindClientByFullName(fullName);
        return (Client)client.Clone();
    }

    /// <summary>
    /// Retrieves a list of all clients.
    /// </summary>
    /// <returns>A list of cloned client objects.</returns>
    public List<Client> GetAllClients()
    {
        return _clients.Select(client => (Client)client.Clone()).ToList();
    }

    /// <summary>
    /// Sorts the client list by first name.
    /// </summary>
    public void SortClientsByFirstName()
    {
        _clients.Sort(new ClientFisrtNameComparer());
    }

    /// <summary>
    /// Sorts the client list by last name.
    /// </summary>
    public void SortClientsByLastName()
    {
        _clients.Sort(new ClientLastNameComparer());
    }
}