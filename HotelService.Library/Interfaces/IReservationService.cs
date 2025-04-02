using HotelService.Library.Models;

namespace HotelService.Library.Interfaces;

public interface IReservationService
{
    public void AddReservation(Reservation reservation);
    public void CancelReservation(Hotel hotel, DateTime startDate, DateTime endDate, int roomNumber);

    public void ChangeReservationDescription(Hotel hotel, DateTime startDate, DateTime endDate, int roomNumber, string description);
    public List<Reservation> GetAllReservationsByFullName(string fullName);
    public List<Reservation> GetAllReservationsByHotelName(string hotelName);
    public List<Reservation> GetAllReservationsByRoomNumber(int roomNumber, Hotel hotel);
}