namespace Salonbeauty
{
    internal class Program
    {

        class User
        {
            private string? name;
            private string? email;
            private string? phoneNumber;
            private string? password;
        }

        class Service
        {
            private string? serviceType;
            private string? details;
        }

        class Booking
        {
            private User? user;
            private Service? service;
            private DateTime bookingDateTime;
        }

        class SalonSystem
        {
            private User? user;
            //Register
            //Login
            //BookService
            //Viewbookedservice
            //Cancelbooking
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //TODO welcome message
            //TODO user registration/login
            //TODO MENU
        }
    }
}
