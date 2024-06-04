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

            //TODO welcome message
            Console.WriteLine("Welcome to the Beauty Salon!");
            //TODO user registration/login
            Console.WriteLine("Please register or login.");
            //NYI register/login
            //TODO MENU
            bool running = true;
            while (running)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Book a service");
                Console.WriteLine("2. View booked services");
                Console.WriteLine("3. Cancel a booking");
                Console.WriteLine("4. Exit");

                
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // TODO Reservation
                        break;
                    case "2":
                        // TODO reservation view
                        break;
                    case "3":
                        // TODO cancellation of reservation
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }
    }
}
