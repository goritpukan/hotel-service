using HotelService.Library;
using HotelService.Library.Models;
using HotelService.Library.Services;

namespace HotelService.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var client1 = new ClientModel("Kuzma", "Skryabin");
        var client2 = new ClientModel("Jane", "Doe");
        var client3 = new ClientModel("John", "Doe");

        var clientService = new ClientService();
        
        clientService.AddClient(client1);
        clientService.AddClient(client2);
        clientService.AddClient(client3);

        var hotel1 = new HotelModel("Hotel1", 300, "Super Hotel");
        var hotel2 = new HotelModel("Hotel2", 600, "Super Hotel again");
        var hotel3 = new HotelModel("Hotel3", 1000, "Super Hotel again");

        var hotelService = new HotelService.Library.Services.HotelService();
        
        hotelService.AddHotel(hotel1);
        hotelService.AddHotel(hotel2);
        hotelService.AddHotel(hotel3);
        
        var date1 = new DateTime(2025, 10, 12);
        var date2 = new DateTime(2025, 11, 12);

        var reservation1 = new ReservationModel(20, "Simple reservation",date1, date2, hotel1, client1);
        var reservation2 = new ReservationModel(24, "Simple reservation",date1, date2, hotel1, client1);
        var reservation3 = new ReservationModel(10, "Simple reservation",date1, date2, hotel1, client2);
        
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
            System.Console.WriteLine($"Name: {hotel.Name} | Capacity: {hotel.Capacity} | Description: {hotel.Description}");
        }
        
        reservationService.CancelReservation(hotel1, date1, date2);
        reservationService.ChangeReservationDescription(hotel1, date1, date2, "Its updated description");
        
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

    private static void PrintReservations(List<ReservationModel> reservations)
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