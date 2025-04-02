using HotelService.Library.Models;
namespace HotelService.Library.Interfaces;

public interface IHotelService
{
    public void AddHotel(Hotel hotel);
    public void DeleteHotelByName(string name);
    public Hotel GetHotelByName(string name);
    public List<Hotel> GetAllHotels();
}