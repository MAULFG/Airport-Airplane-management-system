using System;
using System.Windows.Forms;
using Airport_Airplane_management_system.Model.Interfaces.Views;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class AdminDashboard : UserControl, IAdminDashboardView
    {
        // MVP events
        public event Action ViewLoaded;

        public event Action DashboardClicked;
        public event Action CrewManagementClicked;
        public event Action FlightManagementClicked;
        public event Action PlaneManagementClicked;
        public event Action PassengerManagementClicked;
        public event Action ReportsClicked;
        public event Action AccountSettingsClicked;

        private bool _pagesPrepared;

        public AdminDashboard()
        {
            InitializeComponent();

            // If you have a panel in designer, keep its name.
            // In your old code you had rightPanel.
            // Make sure your designer has: Panel rightPanel;
            // If not, rename it or change it here.
            EnsurePagesInRightPanel();

            HookUiEvents();

            // Fire after control is ready on screen
            this.Load += (_, __) => ViewLoaded?.Invoke();
        }

        private void HookUiEvents()
        {
            // These handler names MUST exist in designer if designer wires them.
            // If your Designer currently shows CS0103 for missing handlers,
            // keep these methods EXACTLY with these names.

            // Buttons/labels clicks:
            // - if you use labels like lblCrewManagement, attach here too.

            // Example (keep ONLY what exists in your designer):
            // btnDashboard.Click += btnDashboard_Click;

            // If your designer already wires events like:
            // this.btnCrewManagement.Click += new System.EventHandler(this.btnCrewManagement_Click);
            // then DO NOT re-add here; it’s fine either way.
        }

        // Presenter calls this: show one page, hide others
        public void ShowPage(Control page)
        {
            EnsurePagesInRightPanel();

            if (page == null) return;

            // hide all pages inside rightPanel
            foreach (Control c in rightPanel.Controls)
                c.Visible = false;

            // show selected
            page.Visible = true;
            page.BringToFront();
        }

        public void ShowError(string message) => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        public void ShowInfo(string message) => MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void EnsurePagesInRightPanel()
        {
            if (_pagesPrepared) return;
            _pagesPrepared = true;

            // These controls MUST exist in your AdminDashboard.Designer.cs:
            // dashboardPage1 (YOUR NEW ONE)
            // crewManagment1
            // flightManagment1
            // planeMangement1
            // passengerManagement1
            // reports1
            // accountSettings1

            MoveIntoRightPanel(dashboard1);
            MoveIntoRightPanel(crewManagment1);
            MoveIntoRightPanel(flightManagment1);
            MoveIntoRightPanel(planeMangement1);
            MoveIntoRightPanel(passengerManagement1);
            MoveIntoRightPanel(reports1);
            MoveIntoRightPanel(accountSettings1);

            // default visible page (dashboard)
            ShowPage(dashboard1);
        }

        private void MoveIntoRightPanel(Control page)
        {
            if (page == null) return;

            page.Dock = DockStyle.Fill;

            // If designer already added it somewhere else, remove first
            if (page.Parent != null && page.Parent != rightPanel)
                page.Parent.Controls.Remove(page);

            if (!rightPanel.Controls.Contains(page))
                rightPanel.Controls.Add(page);

            page.Visible = false;
        }

        // ============================
        // THESE METHODS FIX YOUR CS0103
        // (Designer references them)
        // ============================

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            // Let presenter decide what to show; but safe default already set
            ViewLoaded?.Invoke();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            DashboardClicked?.Invoke();
        }

        private void btnCrewManagement_Click(object sender, EventArgs e)
        {
            CrewManagementClicked?.Invoke();
        }

        private void btnFlightManagement_Click(object sender, EventArgs e)
        {
            FlightManagementClicked?.Invoke();
        }

        private void btnPlaneManagement_Click(object sender, EventArgs e)
        {
            PlaneManagementClicked?.Invoke();
        }

        private void btnPassengerManagement_Click(object sender, EventArgs e)
        {
            PassengerManagementClicked?.Invoke();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsClicked?.Invoke();
        }

        private void btnAccountSettings_Click(object sender, EventArgs e)
        {
            AccountSettingsClicked?.Invoke();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            // Your logo click should behave like Dashboard
            DashboardClicked?.Invoke();
        }

        private void planeMangement1_Load(object sender, EventArgs e)
        {
            // Do nothing (keep to satisfy designer if it references it)
        }
    }
}
