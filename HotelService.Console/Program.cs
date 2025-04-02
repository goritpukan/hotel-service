using HotelService.Library.Models;
using HotelService.Library.Services;
using HotelService.Library.Enums;

namespace HotelService.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var client1 = new Client("Kuzma", "Skryabin");
        var client2 = new Client("Jane", "Doe");
        var client3 = new Client("John", "Doe");

        var clientService = new ClientService();
        
        clientService.AddClient(client1);
        clientService.AddClient(client2);
        clientService.AddClient(client3);

        var hotel1 = new Hotel("Hotel1","Super Hotel");
        var hotel2 = new Hotel("Hotel2","Super Hotel again");
        var hotel3 = new Hotel("Hotel3","Super Hotel again");

        var hotelService = new HotelService.Library.Services.HotelService();
        
        hotelService.AddHotel(hotel1);
        hotelService.AddHotel(hotel2);
        hotelService.AddHotel(hotel3);
        
        var roomService = new RoomService();
        
        roomService.AddRooms(hotel1, 200, 500, RoomType.Deluxe);
        roomService.AddRooms(hotel1, 100, 200, RoomType.Standard);
        
        var roomInfo = roomService.GetRoom(hotel1, 30);
        System.Console.WriteLine($"Info for Room 30: Hotel: {roomInfo?.Hotel.Name} | Price: {roomInfo?.Price} | RoomType: {roomInfo?.Type}");
        
        var date1 = new DateTime(2025, 10, 12);
        var date2 = new DateTime(2025, 11, 12);

        var reservation1 = new Reservation(20, "Simple reservation",date1, date2, hotel1, client1);
        var reservation2 = new Reservation(24, "Simple reservation",date1, date2, hotel1, client1);
        var reservation3 = new Reservation(10, "Simple reservation",date1, date2, hotel1, client2);
        
        var reservationService = new ReservationService();
        
        reservationService.AddReservation(reservation1);
        reservationService.AddReservation(reservation2);
        reservationService.AddReservation(reservation3);
        
        clientService.ChangeClientByFullName("John Doe", "BetterJohn", "BetterDoe");
        clientService.DeleteClientByFullName("Jane Doe");
        
        var clients = clientService.GetAllClients();
        
        System.Console.WriteLine("\nAll clients before sorting:");
        foreach (var client in clients)
        {
            System.Console.WriteLine(client.FullName);
        }
        
        clientService.SortClientsByFirstName();
        clients = clientService.GetAllClients();
        
        System.Console.WriteLine("\nAll clients after sorting:");
        foreach (var client in clients)
        {
            System.Console.WriteLine(client.FullName);
        }
        
        hotelService.DeleteHotelByName("Hotel1");
        var hotels = hotelService.GetAllHotels();
        foreach (var hotel in hotels)
        {
            System.Console.WriteLine($"Name: {hotel.Name} | Description: {hotel.Description}");
        }
        
        reservationService.CancelReservation(hotel1, date1, date2, 20);
        reservationService.ChangeReservationDescription(hotel1, date1, date2,24, "Its updated description");
        
        var allReservationsForHotel1 = reservationService.GetAllReservationsByHotelName("Hotel1");
        System.Console.WriteLine("\nAll Reservations for Hotel 1:");

        PrintReservations(allReservationsForHotel1);

        var allReservationsForClient1 = reservationService.GetAllReservationsByFullName("Kuzma Skryabin");
        System.Console.WriteLine("\nAll Reservations for Client 1:");
        
        PrintReservations(allReservationsForClient1);

        System.Console.WriteLine("\nAll Reservations for Room 24:");
        var allReservationsForRoom = reservationService.GetAllReservationsByRoomNumber(24, hotel1);
        
        PrintReservations(allReservationsForRoom);

    }

    private static void PrintReservations(List<Reservation> reservations)
    {
        foreach (var reservation in reservations)
        {
            System.Console.WriteLine($"Room: {reservation.Room} " +
                                     $"| Description: {reservation.Description} " +
                                     $"| Start date: {reservation.StartDate} " +
                                     $"| End date: {reservation.EndDate} |" +
                                     $" Hotel: {reservation.Hotel.Name}");
        }
    }
}