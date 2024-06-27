using System.Xml.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

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
            // dodate javne metode omogucavaju oristum privatnim clanovima klase
            public User GetUser()
            {
                return user;
            }

            public Service GetService()
            {
                return service;
            }

            public DateTime GetBookingDateTime()
            {
                return bookingDateTime;
            }

        }

        class SalonSystem
        {
            private const string UsersFilePath = "users.txt";
            private const string BookingsFilePath = "bookings.txt";

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
            //added
            public List<Booking> Bookings
            {
                get { return bookings; }
            }

            public User? LoggedInUser
            {
                get { return loggedInUser; }
                set { loggedInUser = value; }
            }

            public SalonSystem()
            {
                
                users = LoadUsers();//poziva metodu load users
                services = new List<Service>();
                bookings =  LoadBookings();
                loggedInUser = null;

                
            }
            
            // Loading the user from a text file
            private List<User> LoadUsers()
            {
                List<User> users = new List<User>();
                if (File.Exists(UsersFilePath)) 
                {
                    string[] lines = File.ReadAllLines(UsersFilePath);
                    
                    foreach (string line in lines)
                    {
                        string[] data = line.Split(',');
                        if (data.Length == 4)
                        {
                            users.Add(new User(data[0], data[1], data[2], data[3]));
                        }
                    }
                }
                return users;
            }
            // save user 
            public void SaveUsers()
            {
                var lines = new List<string>();
                foreach (var user in Users)
                {
                    lines.Add($"{user.Name},{user.Email},{user.PhoneNumber},{user.Password}");
                }
                File.WriteAllLines(UsersFilePath, lines);
            }

           

            private List<Booking> LoadBookings()
            {
                List<Booking> loadedBookings = new List<Booking>();
                if (File.Exists(BookingsFilePath))
                {
                    string[] lines = File.ReadAllLines(BookingsFilePath);
                    foreach (string line in lines)
                    {
                        string[] data = line.Split(',');
                        if (data.Length == 4)
                        {
                            User user = users.FirstOrDefault(u => u.Email == data[0]);
                            if (user != null)
                            {
                                Service service = new Service(data[1], data[2]);
                                DateTime bookingDateTime = DateTime.Parse(data[3]);
                                loadedBookings.Add(new Booking(user, service, bookingDateTime));
                            }
                        }
                    }
                }
                bookings = loadedBookings;
                return loadedBookings;
            }




            public  void SaveBookings()
            {
                var lines = new List<string>();
                foreach (var booking in bookings)
                {
                    lines.Add($"{booking.GetUser().Email},{booking.GetService().ServiceType},{booking.GetService().Details},{booking.GetBookingDateTime()}");
                }
                File.WriteAllLines(BookingsFilePath, lines);
            }

        }

        static void RegisterUser(SalonSystem salonSystem)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            if (!ValidName(name))
            {
                Console.WriteLine("Invalid name. Name must contain only letters.");
                return;
            }
            string email;

            do
            {
                Console.Write("Enter your email: ");
                email = Console.ReadLine();
                if (!ValidEmail(email))
                {
                    Console.WriteLine("Invalid email address.");
                }
            } while (!ValidEmail(email)); // Repeat the entry until the email is valid.

            string phoneNumber;
            do
            {
                Console.Write("Enter your phone number: ");
                phoneNumber = Console.ReadLine();
                if (!ValidPhoneNumber(phoneNumber))
                {
                    Console.WriteLine("Invalid phone number. Phone number must contain only numbers.");
                }
            } while (!ValidPhoneNumber(phoneNumber)); // Ponovi unos dok broj telefona nije validan.

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            User newUser = new User(name, email, phoneNumber, password);
            salonSystem.Users.Add(newUser);
            //added 
            salonSystem.SaveUsers();
            Console.WriteLine("Registration successful.");
        }

        static void LoginUser(SalonSystem salonSystem)
        {
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            // FirstOrDefault
            //Lambda
            User user = salonSystem.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

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

       

        static void BookService(SalonSystem salonSystem)
        {
            // Provera da li je korisnik prijavljen
            if (salonSystem.LoggedInUser == null)
            {
                Console.WriteLine("You must be logged in to book a service.");
                return;
            }

            // Prikaz dostupnih usluga
            Console.WriteLine("Available services::");
            for (int i = 0; i < salonSystem.Services.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {salonSystem.Services[i].ServiceType} - {salonSystem.Services[i].Details}");
            }

            // Odabir usluge
            Console.Write("Enter the number of the service you want to book: ");
            int serviceChoice;
            if (!int.TryParse(Console.ReadLine(), out serviceChoice) || serviceChoice < 1 || serviceChoice > salonSystem.Services.Count)
            {
                Console.WriteLine("Invalid service choice. Please try again.");
                return;
            }
            Service selectedService = salonSystem.Services[serviceChoice - 1];

            // Unos datuma i vremena rezervacije
            Console.Write("Enter the date and time for the booking (yyyy-MM-dd HH:mm): ");
            DateTime bookingDateTime;
            if (!DateTime.TryParse(Console.ReadLine(), out bookingDateTime))
            {
                Console.WriteLine("Invalid date and time format. Please try again.");
                return;
            }

            // Provera dostupnosti
            bool isAvailable = true;
            foreach (var booking in salonSystem.Bookings)
            {
                if (booking.GetBookingDateTime() == bookingDateTime)
                {
                    isAvailable = false;
                    break;
                }
            }

            // Ako je dostupno, kreiranje nove rezervacije
            if (isAvailable)
            {
                Booking newBooking = new Booking(salonSystem.LoggedInUser, selectedService, bookingDateTime);
                salonSystem.Bookings.Add(newBooking);
                salonSystem.SaveBookings();
                Console.WriteLine("The service has been successfully booked.");
            }
            else
            {
                Console.WriteLine("The selected time is not available. Please select another time.");
            }
        }

        static void ViewBookedServices(SalonSystem salonSystem)
        {
            // Provjera da li je korisnik prijavljen
            if (salonSystem.LoggedInUser == null)
            {
                Console.WriteLine("You must be logged in to view booked services.");
                return;
            }

            // Prikaz svih rezervacija za prijavljenog korisnika
            Console.WriteLine("Your booked services:");
            bool foundBookings = false; // Dodajte ovu varijablu za proveru da li su pronađene rezervacije

            foreach (var booking in salonSystem.Bookings)
            {
                if (booking.GetUser().Email == salonSystem.LoggedInUser.Email)
                {
                    Console.WriteLine($"{booking.GetService().ServiceType} - {booking.GetService().Details} at {booking.GetBookingDateTime()}");
                    foundBookings = true;
                }
            }

            if (!foundBookings)
            {
                Console.WriteLine("You have no booked services.");
            }
            
        }

        
        static void CancelBooking(SalonSystem salonSystem)
        {
            // if user is not  logged in
            if (salonSystem.LoggedInUser == null)
            {
                Console.WriteLine("You must be logged in to view booked services.");
                return;
            }

            //show all bookings for the logged in user
            Console.WriteLine("Your booked services:");
            int index = 1;
            var userBookings = salonSystem.Bookings.Where(booking => booking.GetUser() == salonSystem.LoggedInUser).ToList();

            if (userBookings.Count == 0)
            {
                Console.WriteLine("You have no booked services.");
                return;
            }

            foreach (var booking in userBookings)
            {
                Console.WriteLine($"{index}. {booking.GetService().ServiceType} - {booking.GetService().Details} at {booking.GetBookingDateTime()}");
                index++;
            }
           

            //allow user to select a booking to cancel
            Console.Write("Enter the number of the booking you want to cancel: ");
            int bookingChoice = int.Parse(Console.ReadLine());
            if (bookingChoice < 1 || bookingChoice >= index)
            {
                Console.WriteLine("Invalid booking choice. Please try again.");
                return;
            }

            //find reservation
            index = 1;
            Booking bookingToRemove = null;
            foreach (var booking in salonSystem.Bookings)
            {
                if (booking.GetUser() == salonSystem.LoggedInUser)
                {
                    if (index == bookingChoice)
                    {
                        bookingToRemove = booking;
                        break;
                    }
                    index++;
                }
            }

            // Remove reservation
           
            if (bookingToRemove != null)
            {
                Console.Write($"Are you sure you want to cancel the booking for {bookingToRemove.GetService().ServiceType} at {bookingToRemove.GetBookingDateTime()}? (yes/no): ");
                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "yes")
                {
                    salonSystem.Bookings.Remove(bookingToRemove);
                    salonSystem.SaveBookings();

                    Console.WriteLine("Booking canceled successfully.");
                }
                else
                {
                    Console.WriteLine("Cancellation aborted.");
                }
            }

           
        }

        static void ShowMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Book a service");
            Console.WriteLine("4. View booked services");
            Console.WriteLine("5. Cancel a booking");
            Console.WriteLine("6. Exit");
        }

        static bool ValidEmail(string email)
        {
            // Regular Expression to Check the Validity of an E - Mail Address
            //@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
        static bool ValidName(string name)
        {
            return name.All(char.IsLetter);
        }

        static bool ValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.All(char.IsDigit);
        }
        static void Main(string[] args)
        {

            
            Console.WriteLine("Welcome to the Beauty Salon!");
            Console.WriteLine("Write 'help' for help!");
            SalonSystem salonSystem = new SalonSystem();
            
            //NEW adding services to the sitem
            salonSystem.Services.Add(new Service("Manicure", "Basic manicure service"));
            salonSystem.Services.Add(new Service("Manicure", "Strengthening nails"));
            salonSystem.Services.Add(new Service("Pedicure", "Pedicure"));



            bool running = true;
           
            while (running)
            {
               

                
                string choice = Console.ReadLine();

                if (choice.ToLower() == "help")
                {
                    ShowMenu();
                   
                    choice = Console.ReadLine();
                }

                if (choice == "1")
                {

                    RegisterUser(salonSystem);

                }

                 else if (choice == "2")
                {

                    LoginUser(salonSystem);

                 }

                 else if (choice == "3")
                {

                    BookService(salonSystem);

                }

                else if (choice == "4")
                {

                    ViewBookedServices(salonSystem);
                }
                else  if (choice == "5")
                {
                    CancelBooking(salonSystem);
                }

                else  if (choice == "6")
                {
                    salonSystem.SaveUsers();
                    salonSystem.SaveBookings();
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

