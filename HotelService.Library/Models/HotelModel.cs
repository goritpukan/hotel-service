using HotelService.Library.Exceptions;

namespace HotelService.Library.Models;

public class HotelModel : ICloneable
{
    public string Name { get; }
    public int Capacity { get;  }
    public string Description { get; }
    
    public HotelModel(string name, int capacity, string description)
    {
        ValidateConstructorInputs(name, capacity, description);
        Name = name;
        Capacity = capacity;
        Description = description;
    }

    private static void ValidateConstructorInputs(string name, int capacity, string description)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Hotel name cannot be null or empty");
        if(capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero");
        if(string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "Description cannot be null or empty");
    }

    public object Clone()
    {
        return new HotelModel(Name, Capacity, Description);
    }

    public override bool Equals(object? obj)
    {
        return obj is HotelModel other && Name == other.Name; // other variable with type HotelModel
    }

    public override int GetHashCode() => Name.GetHashCode(); //Method for correct work with hash-based tables 

    public static bool operator ==(HotelModel? left, HotelModel? right)
    {
        if (ReferenceEquals(left, right)) return true; 
        if (left is null || right is null) return false; 
        return left.Equals(right);
    }

    public static bool operator !=(HotelModel? left, HotelModel? right)
    {
        return !(left == right);
    }
}