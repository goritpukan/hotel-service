using HotelService.Library.Exceptions;
using HotelService.Library.Models;

namespace HotelService.Library.Services;

public class HotelService
{
    //Aggregation
    private readonly List<HotelModel> _hotels = new List<HotelModel>();

    public void AddHotel(HotelModel hotel)
    {
        if (hotel is null)
            throw new ArgumentNullException(nameof(hotel), "Hotel cannot be null");
        
        _hotels.Add(hotel);
    }

    private HotelModel FindHotelByName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Hotel name cannot be null or empty");
        
        var hotel = _hotels.FirstOrDefault(hotel => hotel.Name == name);
        if (hotel is null)
            throw new HotelNotFoundException(name);
        
        return hotel;
    }

    public void DeleteHotelByName(string name)
    {
       var hotel = FindHotelByName(name);
        _hotels.Remove(hotel);
    }

    public HotelModel GetHotelByName(string name)
    {
       var hotel = FindHotelByName(name);
        return (HotelModel)hotel.Clone();
    }

    public List<HotelModel> GetAllHotels() 
    {
        return _hotels.Select(hotel => (HotelModel)hotel.Clone()).ToList();
    }
    
}