using HotelService.Library.Exceptions;
using HotelService.Library.Models;

namespace HotelService.Library.Services;

public class ReservationService
{
    private readonly List<ReservationModel> _reservations = new List<ReservationModel>();

    public void AddReservation(ReservationModel reservation)
    {
        if(reservation is null)
            throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null");
        ValidateRoom(reservation);
        
        _reservations.Add(reservation);
    }
    
    private void ValidateRoom(ReservationModel reservation)
    {
        if(_reservations.Any(request => 
               request.Room == reservation.Room && 
               reservation.StartDate < request.EndDate && request.StartDate < reservation.EndDate))
        {
            throw new AlreadyReservedException(reservation.Room);
        }
        
        if (reservation.Room > reservation.Hotel.Capacity)
            throw new RoomCapacityExceededException(reservation.Room, reservation.Hotel.Capacity);
    }

    private ReservationModel FindReservation(HotelModel hotel, DateTime startDate, DateTime endDate)
    {
        if(hotel is null)
            throw new ArgumentNullException(nameof(hotel), "Hotel cannot be null");
        
        var reservation = _reservations.FirstOrDefault(reservation => 
            reservation.Hotel == hotel &&
            reservation.StartDate == startDate &&
            reservation.EndDate == endDate);
        if (reservation is null)
            throw new ReservationNotFoundException();
        return reservation;
    }
    
    public void CancelReservation(HotelModel hotel, DateTime startDate, DateTime endDate)
    {
        var reservation = FindReservation(hotel, startDate, endDate);
        _reservations.Remove(reservation);
    }

    public void ChangeReservationDescription(HotelModel hotel, DateTime startDate, DateTime endDate, string description)
    {
        var reservation = FindReservation(hotel, startDate, endDate);
        reservation.Update(description);
    }
    
    public List<ReservationModel> GetAllReservationsByFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName)) 
            throw new ArgumentNullException(nameof(fullName), "Full name cannot be null");
        
        return _reservations
            .Where(reservation => reservation.Client.FullName == fullName)
            .Select(reservation => (ReservationModel)reservation.Clone())
            .ToList();
    }

    public List<ReservationModel> GetAllReservationsByHotelName(string hotelName)
    {
        if(string.IsNullOrWhiteSpace(hotelName))
            throw new ArgumentNullException(nameof(hotelName), "Hotel name cannot be null");
        
        return _reservations
            .Where(reservation => reservation.Hotel.Name == hotelName)
            .Select(reservation => (ReservationModel)reservation.Clone())
            .ToList();
    }

    public List<ReservationModel> GetAllReservationsByRoomNumber(int roomNumber, HotelModel hotel)
    {
        if(roomNumber < 0)
            throw new ArgumentOutOfRangeException(nameof(roomNumber), "Room number cannot be negative");
        if(hotel is null)
            throw new ArgumentNullException(nameof(hotel), "Hotel cannot be null");
        
        return _reservations
            .Where(reservation => reservation.Hotel == hotel && reservation.Room == roomNumber)
            .Select(reservation => (ReservationModel)reservation.Clone())
            .ToList();
    }
}