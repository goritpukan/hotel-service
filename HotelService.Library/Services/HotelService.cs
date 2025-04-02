using HotelService.Library.Exceptions;
using HotelService.Library.Interfaces;
using HotelService.Library.Models;

namespace HotelService.Library.Services;

public class HotelService : IHotelService
{
    //Aggregation
    private readonly List<Hotel> _hotels = new List<Hotel>();

    /// <summary>
    /// Adds a hotel to the collection.
    /// </summary>
    /// <param name="hotel">The hotel to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when hotel is null.</exception>
    public void AddHotel(Hotel hotel)
    {
        if (hotel is null)
            throw new ArgumentNullException(nameof(hotel), "Hotel cannot be null");
        
        _hotels.Add(hotel);
    }
    
    /// <summary>
    /// Finds a hotel by its name.
    /// </summary>
    /// <param name="name">The name of the hotel to find.</param>
    /// <returns>The hotel with the specified name.</returns>
    /// <exception cref="ArgumentNullException">Thrown when name is null or empty.</exception>
    /// <exception cref="HotelNotFoundException">Thrown when no hotel with the specified name is found.</exception>
    private Hotel FindHotelByName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Hotel name cannot be null or empty");
        
        var hotel = _hotels.FirstOrDefault(hotel => hotel.Name == name);
        if (hotel is null)
            throw new HotelNotFoundException(name);
        
        return hotel;
    }

    /// <summary>
    /// Deletes a hotel from the collection by its name.
    /// </summary>
    /// <param name="name">The name of the hotel to delete.</param>
    /// <exception cref="HotelNotFoundException">Thrown when no hotel with the specified name is found.</exception>
    /// <exception cref="ArgumentNullException">Thrown when name is null or empty.</exception>
    public void DeleteHotelByName(string name)
    {
       var hotel = FindHotelByName(name);
        _hotels.Remove(hotel);
    }

    /// <summary>
    /// Gets a copy of a hotel by its name.
    /// </summary>
    /// <param name="name">The name of the hotel to retrieve.</param>
    /// <returns>A clone of the hotel with the specified name.</returns>
    /// <exception cref="HotelNotFoundException">Thrown when no hotel with the specified name is found.</exception>
    /// <exception cref="ArgumentNullException">Thrown when name is null or empty.</exception>
    public Hotel GetHotelByName(string name)
    {
       var hotel = FindHotelByName(name);
        return (Hotel)hotel.Clone();
    }

    /// <summary>
    /// Gets a list containing copies of all hotels in the collection.
    /// </summary>
    /// <returns>A new list with clones of all hotels.</returns>
    public List<Hotel> GetAllHotels() 
    {
        return _hotels.Select(hotel => (Hotel)hotel.Clone()).ToList();
    }
    
}