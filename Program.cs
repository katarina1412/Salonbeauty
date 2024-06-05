namespace Salonbeauty
{
    internal class Program
    {

        class User
        {
            private string name, email, phoneNumber, password;
            

            public User(string name, string email, string phoneNumber, string password)
            {
                this.name = name;
                this.email = email;
                this.phoneNumber = phoneNumber;
                this.password = password;
            }
        }

        class Service
        {
            private string serviceType, details;

            public Service(string serviceType, string details)
            {
                this.serviceType = serviceType;
                this.details = details;
            }


        }

        class Booking
        {
            private User user; Service service; DateTime bookingDateTime;
            

            public Booking(User user, Service service, DateTime bookingDateTime)
            {
                this.user = user;
                this.service = service;
                this.bookingDateTime = bookingDateTime;
            }

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

            
            Console.WriteLine("Welcome to the Beauty Salon!");
            
            
            bool running = true;
            while (running)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Book a service");
                Console.WriteLine("4. View booked services");
                Console.WriteLine("5. Cancel a booking");
                Console.WriteLine("6. Exit");

                
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    //NYI Registration

                }

                 else if (choice == "2")
                {
                    //NYI Login
                }

                 else if (choice == "3")
                {
                    //NYI book a service 
                }

                 else if (choice == "4")
                {
               
                    //NYI View booked services
                }
                 else  if (choice == "5")
                {
                    //NYI  Cancel a booking
                }

                 else  if (choice == "6")
                {
                    //NYI  Exit
                }

                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }

        }
    }
}
