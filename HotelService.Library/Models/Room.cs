using HotelService.Library.Enums;

namespace HotelService.Library.Models;

public class Room : ICloneable
{
    public int Number { get; }
    public RoomType Type { get; }
    public decimal Price { get; }
    public Hotel Hotel { get; }

    public Room(int number, RoomType type, decimal price, Hotel hotel)
    {
        ValidateConstructor(number, price);
        Number = number;
        Type = type;
        Price = price;
        Hotel = hotel;
    }

    private void ValidateConstructor(int number,decimal price)
    {
        if(number < 0)
            throw new ArgumentException("Invalid room number", nameof(number));
        if(price < 0)
           throw new ArgumentException("Invalid room price", nameof(price));
    }

    public override bool Equals(object? obj)
    {
        return obj is Room other && Number == other.Number && Type == other.Type && Price == other.Price && Hotel == other.Hotel;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Number, Type, Price, Hotel);
    }

    public static bool operator ==(Room? left, Room? right)
    {
        if (ReferenceEquals(left, right)) return true; 
        if (left is null || right is null) return false; 
        return left.Equals(right);
    }

    public static bool operator !=(Room? left, Room? right)
    {
        return !(left == right);
    }

    public object Clone()
    {
        return new Room(Number, Type, Price, (Hotel)Hotel.Clone());
    }
}