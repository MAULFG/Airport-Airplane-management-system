using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter;
using Airport_Airplane_management_system.View.Forms.UserPages.Booking_pages;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class BookingPage : Form, IBookingView
    {
        public event Action<FlightSeats> SeatSelected;
        public event Action ConfirmBookingClicked;
        private readonly BookingService bookingService;
        private readonly PassengerService passengerService;
        private readonly IAppSession session;
        public BookingPage(int flightId,BookingService bookingService,PassengerService passengerService,IAppSession session)
        {
            InitializeComponent();

            this.bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
            this.passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));
            this.session = session ?? throw new ArgumentNullException(nameof(session));
            passengerDetails1.Hide();
            btnConfirm.Click += (_, _) => ConfirmBookingClicked?.Invoke();
        }

        private Flight _currentFlight;

        public void ShowPassengerDetails(Flight flight, FlightSeats seat, decimal price)
        {
            passengerDetails1.Show();
            passengerDetails1.BringToFront();

            // Attach the presenter to the existing control
            var presenter = new PassengerDetailsPresenter(passengerDetails1, passengerService, bookingService, session, flight, seat, price);
            presenter.PassengerDetailsClosed += () =>
            {
                passengerDetails1.Hide();
                ShowSeats(flight); // refresh the seat map
            };

            
        }




        public void ShowFlightInfo(Flight flight)
        {
            lblFlightInfo.Text =
                $"{flight.From} → {flight.To} | " +
                $"Departure: {flight.Departure:g} | " +
                $"Arrival: {flight.Arrival:g}";
        }

        public void ShowSeats(Flight flight)
        {
            flowSeats.Controls.Clear();

            if (flight?.FlightSeats == null || flight.FlightSeats.Count == 0)
            {
                ShowMessage("No seats available for this flight.");
                return;
            }

            var seatDict = flight.FlightSeats.ToDictionary(s => s.SeatNumber);

            Dictionary<int, string> firstMap = null;
            Dictionary<int, string> businessMap = null;
            Dictionary<int, string> economyMap = null;
            int totalColumns = 0;

            string model = flight.Plane.Model.ToLower();

            if (model.Contains("a320"))
            {
                totalColumns = 9;
                firstMap = null;
                businessMap = new Dictionary<int, string> { { 2, "A" }, { 3, "B" }, { 5, "C" }, { 6, "D" } };
                economyMap = new Dictionary<int, string> { { 1, "A" }, { 2, "B" }, { 3, "C" }, { 5, "D" }, { 6, "E" }, { 7, "F" } };
            }
            else if (model.Contains("777"))
            {
                totalColumns = 11;
                firstMap = new Dictionary<int, string> { { 2, "A" }, { 4, "B" }, { 6, "C" }, { 8, "D" } };
                businessMap = new Dictionary<int, string> { { 1, "A" }, { 2, "B" }, { 4, "C" }, { 6, "D" }, { 8, "E" }, { 9, "F" } };
                economyMap = new Dictionary<int, string> { { 0, "A" }, { 1, "B" }, { 2, "C" }, { 4, "D" }, { 5, "E" }, { 6, "F" }, { 8, "G" }, { 9, "H" }, { 10, "J" } };
            }
            else if (model.Contains("g650"))
            {
                // G650 is a private jet, usually 1-1 or 2-2 seating
                totalColumns = 4; // simple layout
                firstMap = new Dictionary<int, string> { { 0, "V" }, { 1, "V" } };
                businessMap = null;
                economyMap = null; // usually not used
            }else
            {
                totalColumns = 9;
                firstMap = new Dictionary<int, string> { { 2, "A" }, { 3, "B" }, { 4, "C" }, { 5, "D" } };
                businessMap = new Dictionary<int, string> { { 1, "A" }, { 2, "B" }, {3, "C" }, { 4, "D" }, { 5, "E" }, { 6, "F" } };
                economyMap = new Dictionary<int, string> { { 0, "A" }, { 1, "B" }, { 2, "C" }, { 3, "D" }, { 4, "E" }, { 5, "F" }, { 6, "G" }, { 7, "H" }, { 8, "I" } };

            }
            _currentFlight = flight;
            // Create table
            var table = new TableLayoutPanel
            {
                RowCount = model.Contains("g650") ? 6 : 43,
                ColumnCount = totalColumns,
                AutoSize = true
            };

            for (int c = 0; c < totalColumns; c++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));

            for (int r = 0; r < 43; r++)
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            // Track which row is used in table
            int currentRow = 0;

            void FillClass(Dictionary<int, string> map, string classType)
            {
                if (map == null || map.Count == 0) return;

                // Only seats of this class
                var seats = flight.FlightSeats
                                  .Where(s => s.ClassType.Equals(classType, StringComparison.OrdinalIgnoreCase))
                                  .OrderBy(s => s.SeatNumber)
                                  .ToList();

                if (!seats.Any()) return;

                // Group seats by row number (digits part of seatNumber)
                var groupedRows = new Dictionary<int, List<FlightSeats>>();
                foreach (var seat in seats)
                {
                    int rowNum = int.Parse(new string(seat.SeatNumber.TakeWhile(char.IsDigit).ToArray()));
                    if (!groupedRows.ContainsKey(rowNum))
                        groupedRows[rowNum] = new List<FlightSeats>();
                    groupedRows[rowNum].Add(seat);
                }

                // Fill each row that has seats
                foreach (var rowNum in groupedRows.Keys.OrderBy(x => x))
                {
                    foreach (var kv in map)
                    {
                        string seatNumber = $"{rowNum}{kv.Value}";
                        AddSeatOrPlaceholder(table, seatDict, seatNumber, currentRow, kv.Key);
                    }
                    currentRow++;
                }
            }

            // Fill only classes that exist
            FillClass(firstMap, "First");
            FillClass(businessMap, "Business");
            FillClass(economyMap, "Economy");

            var wrapper = new Panel { AutoSize = true };
            wrapper.Controls.Add(table);
            wrapper.Margin = new Padding((flowSeats.Width - table.PreferredSize.Width) / 2 - 200, 10, 10, 10);
            flowSeats.Controls.Add(wrapper);
        }





        // Helper function
        private void AddSeatOrPlaceholder(TableLayoutPanel table, Dictionary<string, FlightSeats> seatDict, string seatNumber, int row, int col)
        {
            if (seatDict.TryGetValue(seatNumber, out var seat))
            {
                table.Controls.Add(CreateSeatButton(seatDict, seatNumber), col, row);
            }
            else
            {
                table.Controls.Add(new Panel { Width = 65, Height = 45 }, col, row); // empty placeholder
            }
        }

        private Control CreateSeatButton(Dictionary<string, FlightSeats> seatDict, string seatNumber)
        {
            if (!seatDict.TryGetValue(seatNumber, out FlightSeats seat))
            {
                return new Panel { Width = 65, Height = 45 }; // empty space if seat missing
            }

            var btn = new Guna2Button
            {
                Width = 65,
                Height = 45,
                BorderRadius = 8,
                Text = seat.SeatNumber,
                FillColor = seat.IsBooked ? Color.Gray : GetColor(seat.ClassType),
                Enabled = !seat.IsBooked,
                Tag = seat
            };
            btn.Click += (_, _) => SeatSelected?.Invoke(seat);
            return btn;
        }
        public void ShowSelectedSeat(FlightSeats seat, decimal ignoredBasePrice = 0)
        {
            lblStatusValue.Text = seat.IsBooked ? "Booked" : "Available";
            lblSeatValue.Text = seat.SeatNumber;
            lblClassValue.Text = seat.ClassType;

            decimal price = seat.SeatPrice; // ✅ always use the seat-specific price
            decimal tax = price * 0.10m;
            decimal total = price + tax;

            // Optional: window seat surcharge
            if (IsWindowSeat(seat, _currentFlight))
                total += price * 0.20m;

            lblBasePriceValue.Text = $"Base: ${price:0.00}";
            lblTaxValue.Text = $"Tax: ${tax:0.00}";
            lblTotalValue.Text = $"Total: ${total:0.00}";
        }



        private bool IsWindowSeat(FlightSeats seat, Flight flight)
        {
            string seatLetter = new string(seat.SeatNumber
                                            .SkipWhile(char.IsDigit)
                                            .ToArray())
                                            .ToUpper();

            string model = flight.Plane.Model.ToLower();

            if (model.Contains("a320"))
            {
                return seatLetter == "A" || seatLetter == "F";
            }
            else if (model.Contains("777"))
            {
                return seat.ClassType switch
                {
                    "First" => seatLetter == "A" || seatLetter == "D",
                    "Business" => seatLetter == "A" || seatLetter == "F",
                    "Economy" => seatLetter == "A" || seatLetter == "J",
                    _ => false
                };
            }
            else if (model.Contains("g650"))
            {
                return seatLetter == "V" || seatLetter == "V";
            }
            else
            {
                return seat.ClassType switch
                {
                    "First" => seatLetter == "A" || seatLetter == "D",
                    "Business" => seatLetter == "A" || seatLetter == "F",
                    "Economy" => seatLetter == "A" || seatLetter == "J",
                    _ => false
                };
            }

            
        }




        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private Color GetColor(string classType)
        {
            return classType switch
            {
                "First" => Color.Goldenrod,
                "Business" => Color.SteelBlue,
                _ => Color.DarkCyan
            };
        }

        private void panelSummary_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowSeats_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BookingPage_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
