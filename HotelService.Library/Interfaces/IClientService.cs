using HotelService.Library.Models;
namespace HotelService.Library.Interfaces;

public interface IClientService
{
    public void AddClient(Client client);
    public void DeleteClientByFullName(string fullName);
    public void ChangeClientByFullName(string fullName, string newFirstName, string newLastName);
    public Client GetClientByFullName(string fullName);
    public List<Client> GetAllClients();
    public void SortClientsByFirstName();
    public void SortClientsByLastName();
}