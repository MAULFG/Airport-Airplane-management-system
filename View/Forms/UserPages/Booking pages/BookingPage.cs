using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class BookingPage : Form, IBookingView
    {
        public event Action<FlightSeats> SeatSelected;
        public event Action ConfirmBookingClicked;
         
        public BookingPage(int flightId)
        {
            InitializeComponent();
             int id = flightId;
        btnConfirm.Click += (_, _) => ConfirmBookingClicked?.Invoke();
        }
        
        // A320neo
        int[] A320_FULL = { 1, 1, 0, 1, 1, 0, 1, 1 }; // economy
        int[] A320_BUS_ADJ = { 1, 0, 0, 1, 1, 0, 0, 1 }; // business aligned
       

        // Boeing 777-300
        int[] B777_FULL = { 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1 }; // economy
        int[] B777_BUS_ADJ = { 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0 };
        int[] B777_FST_ADJ = { 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0 };


        private TableLayoutPanel CreateSeatTable(int rows, int[] layoutPattern)
        {
            var table = new TableLayoutPanel
            {
                RowCount = rows,
                ColumnCount = layoutPattern.Length,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            var centerPanel = new Panel
            {
                AutoSize = true,
                Margin = new Padding((flowSeats.Width - table.Width) / 2,10,10,10)
            };
            centerPanel.Controls.Add(table);
            flowSeats.Controls.Add(centerPanel);
            
            foreach (var col in layoutPattern)
            {
                if (col == 0) 
                    table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
                else // seat
                    table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));
            }

            for (int r = 0; r < rows; r++)
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            return table;
        }
        public void ShowFlightInfo(Flight flight)
        {
            lblFlightInfo.Text =
                $"{flight.From} → {flight.To} | " +
                $"Departure: {flight.Departure:g} | " +
                $"Arrival: {flight.Arrival:g}";
        }
        private void FillSeats(TableLayoutPanel table,List<FlightSeats> orderedSeats,int startRow,int rows,int[] pattern,ref int seatIndex)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < pattern.Length; c++)
                {
                    if (pattern[c] == 0)
                    {
                        table.Controls.Add(new Panel { Width = 30 }, c, startRow + r);
                        continue;
                    }

                    if (seatIndex >= orderedSeats.Count)
                        return;

                    var seat = orderedSeats[seatIndex++];

                    table.Controls.Add(
                        CreateSeatButton(
                            orderedSeats.ToDictionary(s => s.SeatNumber),
                            seat.SeatNumber
                        ),
                        c,
                        startRow + r
                    );
                }
            }
        }
        public void ShowSeats(Flight flight)
        {
            MessageBox.Show($"Plane: {flight.Plane.Model}\nSeats count: {flight.FlightSeats.Count}");

            flowSeats.Controls.Clear();
            if (flight?.Plane == null || flight.FlightSeats == null) return;
            var orderedSeats = flight.FlightSeats.OrderBy(s => s.SeatNumber).ToList();

            var seatDict = flight.FlightSeats.Where(s => !string.IsNullOrEmpty(s.SeatNumber)).GroupBy(s => s.SeatNumber).Select(g => g.First()).ToDictionary(s => s.SeatNumber);


            // 1️⃣ Get plane-specific layout
            var layout = GetSeatLayout(flight.Plane.Model);

            // 2️⃣ Create table
            var table = new TableLayoutPanel
            {
                RowCount = layout.Rows,
                ColumnCount = layout.Columns.Length,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            // Set column widths (0 = aisle, 1 = seat)
            foreach (var col in layout.Columns)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, col == 0 ? 30 : 65));

            // Set row heights
            for (int r = 0; r < layout.Rows; r++)
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            // 3️⃣ Fill seats
            int seatIndex = 0;

            for (int r = 0; r < layout.Rows; r++)
            {
                for (int c = 0; c < layout.Columns.Length; c++)
                {
                    if (layout.Columns[c] == 0)
                    {
                        table.Controls.Add(new Panel { Width = 30 }, c, r);
                        continue;
                    }

                    if (seatIndex >= orderedSeats.Count)
                        break;

                    var seat = orderedSeats[seatIndex++];
                    table.Controls.Add(
                        CreateSeatButton(seatDict, seat.SeatNumber),
                        c,
                        r
                    );
                }
            }


            // 4️⃣ Center the table inside flow panel
            var centerPanel = new Panel
            {
                AutoSize = true,
                Dock = DockStyle.Top
            };
            centerPanel.Controls.Add(table);

            flowSeats.Controls.Add(centerPanel);
        }
        private (int Rows, int[] Columns) GetSeatLayout(string planeModel)
        {
            if (string.IsNullOrWhiteSpace(planeModel))
                return (20, new int[] { 1, 1, 1, 1 });

            planeModel = planeModel.ToLower().Trim();

            if (planeModel.Contains("a320"))
            {
                return (31, new int[] { 1, 0, 1, 1, 0, 1, 1 });
            }

            if (planeModel.Contains("777"))
            {
                return (41, new int[] { 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1 });
            }

            // fallback
            return (20, new int[] { 1, 1, 1, 1 });
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
        public void ShowSelectedSeat(FlightSeats seat, decimal price)
        {
            lblSeatDetails.Text =
                $"Seat: {seat.SeatNumber}\n" +
                $"Class: {seat.ClassType}\n" +
                $"Price: ${price}";
        }

        public void ShowPrice(decimal price)
        {
            lblPriceDetails.Text =
                $"Base Price: ${price}\n" +
                $"Taxes: ${(price * 0.1m):0.00}\n" +
                $"Total: ${(price * 1.1m):0.00}";
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
    }
}
