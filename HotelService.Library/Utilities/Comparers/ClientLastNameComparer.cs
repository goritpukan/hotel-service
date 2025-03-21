using HotelService.Library.Models;

namespace HotelService.Library.Utilities.Comparers;

public class ClientLastNameComparer : IComparer<ClientModel>
{
    public int Compare(ClientModel? x, ClientModel? y)
    {
        if (x is null) return -1; // x < y 
        if (y is null) return 1; // x > y
        return string.Compare(x.LastName, y.LastName, StringComparison.Ordinal); //Ordinal == the same sorting rules for different cultural variations
    }
}