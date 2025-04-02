using HotelService.Library.Enums;
using HotelService.Library.Interfaces;
using HotelService.Library.Models;

namespace HotelService.Library.Services;

public class RoomService : IRoomService
{
    /// <summary>
    /// Adds multiple rooms to a hotel with sequential room numbers.
    /// </summary>
    /// <param name="hotel">The hotel to add rooms to.</param>
    /// <param name="count">The number of rooms to add.</param>
    /// <param name="price">The price per night for each room.</param>
    /// <param name="roomType">The type of rooms to add.</param>
    /// <remarks>
    /// Room numbers are assigned sequentially starting from the highest existing room number.
    /// </remarks>
    public void AddRooms(Hotel hotel, int count, decimal price, RoomType roomType)
    {
        int startNumber = hotel.GetHighestRoomNumber();
        for (int i = 0; i < count; i++)
        {
            hotel.AddRoom(new Room(++startNumber, roomType, price, hotel));
        }
    }

    /// <summary>
    /// Retrieves a specific room from a hotel by its room number.
    /// </summary>
    /// <param name="hotel">The hotel to search in.</param>
    /// <param name="roomNumber">The room number to look for.</param>
    /// <returns>The room with the specified number, or null if no matching room is found.</returns>
    /// <exception cref="ArgumentNullException">Thrown when hotel is null.</exception>
    public Room? GetRoom(Hotel hotel, int roomNumber)
    {
        if(hotel is null)
            throw new ArgumentNullException(nameof(hotel), "Hotel cannot be null");
        return hotel.Rooms.FirstOrDefault(room => room.Number == roomNumber);
    }
}