using System.Xml.Linq;

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

            // Properties to access private members
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public string Email
            {
                get { return email; }
                set { email = value; }
            }

            public string PhoneNumber
            {
                get { return phoneNumber; }
                set { phoneNumber = value; }
            }

            public string Password
            {
                get { return password; }
                set { password = value; }
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

            public string ServiceType
            {
                get { return serviceType; }
                set { serviceType = value; }
            }

            public string Details
            {
                get { return details; }
                set { details = value; }
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
            private List<User> users;
            private List<Service> services;
            private List<Booking> bookings;
            private User? loggedInUser;

            public List<User> Users
            {
                get { return users; }
            }

            // added

            public List<Service> Services
            {
                get { return services; }
            }

            public User? LoggedInUser
            {
                get { return loggedInUser; }
                set { loggedInUser = value; }
            }

            public SalonSystem()
            {
                users = new List<User>();
                services = new List<Service>();
                bookings = new List<Booking>();
                loggedInUser = null;

                
            }
           
        }
        static void Main(string[] args)
        {

            
            Console.WriteLine("Welcome to the Beauty Salon!");
            SalonSystem salonSystem = new SalonSystem();
            //NEW adding services to the sitem
            salonSystem.Services.Add(new Service("Manicure", "Basic manicure service"));
            salonSystem.Services.Add(new Service("Manicure", "Strengthening nails"));
            salonSystem.Services.Add(new Service("Pedicure", "Pedicure"));



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
                    //TODO finish registration 
                    Console.Write("Enter your name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter your email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter your phone number: ");
                    string phoneNumber = Console.ReadLine();
                    Console.Write("Enter your password: ");
                    string password = Console.ReadLine();

                    User newUser = new User(name, email, phoneNumber, password);
                    salonSystem.Users.Add(newUser);
                    Console.WriteLine("Registration successful.");

                }

                 else if (choice == "2")
                {
                    //NYI Login
                    Console.Write("Enter your email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter your password: ");
                    string password = Console.ReadLine();

                    // Checking users in the system
                    // FirstOrDefault
                    //Lambda
                    User user = salonSystem.Users.FirstOrDefault(user => user.Email == email);

                    if (user != null)
                    {
                        salonSystem.LoggedInUser = user;
                        Console.WriteLine("Login successful.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid email or password. Please try again.");
                    }
                
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
                   
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }

        }
    }
}
