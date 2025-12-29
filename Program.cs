using Airport_Airplane_management_system.Infrastructure.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View;
namespace Airport_Airplane_management_system
{
    internal static class Program
    {
       

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        

            IUserRepository userRepo = new MySqlUserRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            IFlightRepository flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            IBookingRepository bookingRepo = new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            IPlaneRepository planeRepo = new MySqlPlaneRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            IPassengerRepository passengerRepo = new MySqlPassengerRepository("server=localhost;port=3306;database=user;user=root;password=2006");

            IAppSession session = new AppSession();
          
            var userService = new UserService(userRepo, session);
            var flightService = new FlightService(flightRepo, userRepo,bookingRepo,planeRepo, session);
            var bookingService = new BookingService(bookingRepo, session);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Main1());
        }
    }
}