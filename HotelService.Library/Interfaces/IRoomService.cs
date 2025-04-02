using HotelService.Library.Enums;
using HotelService.Library.Models;

namespace HotelService.Library.Interfaces;

public interface IRoomService
{
    public void AddRooms(Hotel hotel, int count, decimal price, RoomType roomType);
    public Room? GetRoom(Hotel hotel, int roomNumber);
}