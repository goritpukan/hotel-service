using HotelService.Library.Exceptions;

namespace HotelService.Library.Models;

public class Hotel : ICloneable
{
    public string Name { get; }
    
    private readonly List<Room> _rooms = new List<Room>();
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();
    public string Description { get; }
    
    public Hotel(string name, string description)
    {
        ValidateConstructorInputs(name, description);
        Name = name;
        Description = description;
    }

    public void AddRoom(Room room)
    {
        if(room is null)
            throw new ArgumentNullException(nameof(room), "Room cannot be null");
        _rooms.Add(room);
    }

    public int GetHighestRoomNumber()
    {
        if (_rooms.Count == 0)
            return 0;
            
        return _rooms.Max(room => room.Number);
    }

    private static void ValidateConstructorInputs(string name, string description)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Hotel name cannot be null or empty");
        if(string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "Description cannot be null or empty");
    }

    public object Clone()
    {
        return new Hotel(Name, Description);
    }

    public override bool Equals(object? obj)
    {
        return obj is Hotel other && Name == other.Name; // other variable with type HotelModel
    }

    public override int GetHashCode() => Name.GetHashCode(); //Method for correct work with hash-based tables 

    public static bool operator ==(Hotel? left, Hotel? right)
    {
        if (ReferenceEquals(left, right)) return true; 
        if (left is null || right is null) return false; 
        return left.Equals(right);
    }

    public static bool operator !=(Hotel? left, Hotel? right)
    {
        return !(left == right);
    }
}