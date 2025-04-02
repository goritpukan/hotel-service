using HotelService.Library.Models;

namespace HotelService.Library.Utilities.Comparers;

public class ClientFisrtNameComparer : IComparer<Client>
{
    public int Compare(Client? x, Client? y)
    {
        if (x is null) return -1; // x < y 
        if (y is null) return 1; // x > y
        return string.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal); //Ordinal == the same sorting rules for different cultural variations
    }
    
}