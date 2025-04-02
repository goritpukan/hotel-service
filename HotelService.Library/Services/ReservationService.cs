using HotelService.Library.Exceptions;
using HotelService.Library.Interfaces;
using HotelService.Library.Models;

namespace HotelService.Library.Services;

public class ReservationService : IReservationService
{
    private readonly List<Reservation> _reservations = new List<Reservation>();
        
    /// <summary>
    /// Adds a new reservation to the system.
    /// </summary>
    /// <param name="reservation">The reservation to be added.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="reservation"/> parameter is null.
    /// </exception>
    /// <exception cref="AlreadyReservedException">
    /// Thrown when the specified reservation is already reserved.
    /// </exception>
    /// <exception cref="RoomNotFoundException">
    /// Thrown when the room number exceeds the hotel's capacity.
    /// </exception>
    public void AddReservation(Reservation reservation)
    {
        if(reservation is null)
            throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null");
        var room = reservation.Hotel.Rooms.FirstOrDefault(r => r.Number == reservation.Room);
        if (room == null)
            throw new RoomNotFoundException(reservation.Room);
        if(IsReserved(reservation))
            throw new AlreadyReservedException("Room is already reserved");
        _reservations.Add(reservation);
    }
    
    private bool IsReserved(Reservation reservation)
    {
        return _reservations.Any(request =>
            request.Room == reservation.Room &&
            reservation.StartDate < request.EndDate && request.StartDate < reservation.EndDate);

    }

    /// <summary>
    /// Finds a reservation based on the given hotel, date range, and room number.
    /// </summary>
    /// <param name="hotel">The hotel where the reservation was made.</param>
    /// <param name="startDate">The start date of the reservation.</param>
    /// <param name="endDate">The end date of the reservation.</param>
    /// <param name="roomNumber">The room number associated with the reservation.</param>
    /// <returns>The matching reservation.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="hotel"/> is null.
    /// </exception>
    /// <exception cref="ReservationNotFoundException">
    /// Thrown when no matching reservation is found.
    /// </exception>
    private Reservation FindReservation(Hotel hotel, DateTime startDate, DateTime endDate, int roomNumber)
    {
        if(hotel is null)
            throw new ArgumentNullException(nameof(hotel), "Hotel cannot be null");
        
        var reservation = _reservations.FirstOrDefault(reservation => 
            reservation.Hotel == hotel &&
            reservation.StartDate == startDate &&
            reservation.EndDate == endDate &&
            reservation.Room == roomNumber
            );
        if (reservation is null)
            throw new ReservationNotFoundException();
        return reservation;
    }
    
    /// <summary>
    /// Cancels an existing reservation based on the given hotel, date range, and room number.
    /// </summary>
    /// <param name="hotel">The hotel where the reservation was made.</param>
    /// <param name="startDate">The start date of the reservation.</param>
    /// <param name="endDate">The end date of the reservation.</param>
    /// <param name="roomNumber">The room number associated with the reservation.</param>
    /// <exception cref="ReservationNotFoundException">
    /// Thrown when no matching reservation is found.
    /// </exception>
    public void CancelReservation(Hotel hotel, DateTime startDate, DateTime endDate, int roomNumber)
    {
        var reservation = FindReservation(hotel, startDate, endDate, roomNumber);
        _reservations.Remove(reservation);
    }
    
    /// <summary>
    /// Changes the description of an existing reservation.
    /// </summary>
    /// <param name="hotel">The hotel where the reservation was made.</param>
    /// <param name="startDate">The start date of the reservation.</param>
    /// <param name="endDate">The end date of the reservation.</param>
    /// <param name="roomNumber">The room number associated with the reservation.</param>
    /// <param name="description">The new description to set.</param>
    /// <exception cref="ReservationNotFoundException">
    /// Thrown when no matching reservation is found.
    /// </exception>
    public void ChangeReservationDescription(Hotel hotel, DateTime startDate, DateTime endDate, int roomNumber, string description)
    {
        var reservation = FindReservation(hotel, startDate, endDate, roomNumber);
        reservation.Update(description);
    }
    
    /// <summary>
    /// Retrieves all reservations made by a specific client.
    /// </summary>
    /// <param name="fullName">The full name of the client.</param>
    /// <returns>A list of reservations associated with the given client.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="fullName"/> is null or empty.
    /// </exception>
    public List<Reservation> GetAllReservationsByFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName)) 
            throw new ArgumentNullException(nameof(fullName), "Full name cannot be null");
        
        return _reservations
            .Where(reservation => reservation.Client.FullName == fullName)
            .Select(reservation => (Reservation)reservation.Clone())
            .ToList();
    }

    /// <summary>
    /// Retrieves all reservations for a specific hotel.
    /// </summary>
    /// <param name="hotelName">The name of the hotel.</param>
    /// <returns>A list of reservations associated with the given hotel.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="hotelName"/> is null or empty.
    /// </exception>
    public List<Reservation> GetAllReservationsByHotelName(string hotelName)
    {
        if(string.IsNullOrWhiteSpace(hotelName))
            throw new ArgumentNullException(nameof(hotelName), "Hotel name cannot be null");
        
        return _reservations
            .Where(reservation => reservation.Hotel.Name == hotelName)
            .Select(reservation => (Reservation)reservation.Clone())
            .ToList();
    }
    
    /// <summary>
    /// Retrieves all reservations for a specific room number in a hotel.
    /// </summary>
    /// <param name="roomNumber">The room number to filter reservations.</param>
    /// <param name="hotel">The hotel where the reservations were made.</param>
    /// <returns>A list of reservations associated with the given room number and hotel.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="roomNumber"/> is negative.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="hotel"/> is null.
    /// </exception>

    public List<Reservation> GetAllReservationsByRoomNumber(int roomNumber, Hotel hotel)
    {
        if(roomNumber < 0)
            throw new ArgumentOutOfRangeException(nameof(roomNumber), "Room number cannot be negative");
        if(hotel is null)
            throw new ArgumentNullException(nameof(hotel), "Hotel cannot be null");
        
        return _reservations
            .Where(reservation => reservation.Hotel == hotel && reservation.Room == roomNumber)
            .Select(reservation => (Reservation)reservation.Clone())
            .ToList();
    }
}