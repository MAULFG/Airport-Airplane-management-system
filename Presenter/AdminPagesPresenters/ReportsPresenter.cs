using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.AdminPagesPresenters
{
    public class ReportsPresenter
    {
        private readonly IReportsView _view;
        private readonly ReportsService _service;

        private List<ReportItemRow> _all = new();

        public ReportsPresenter(IReportsView view, ReportsService service)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));

            // Bind events
            _view.ViewLoaded += Load;
            _view.SearchChanged += OnSearch;
        }

        // 🔥 Public method to refresh reports
        public void RefreshData()
        {
            Load();
        }

        private void Load()
        {
            try
            {
                _all = _service.GetReportItems() ?? new List<ReportItemRow>();
                _view.SetHeaderCounts(_all.Count);
                _view.RenderReports(_all);
            }
            catch (Exception ex)
            {
                _all = new List<ReportItemRow>();
                _view.SetHeaderCounts(0);
                _view.RenderReports(_all);
                _view.ShowError("Failed to load reports.\n" + ex.Message);
            }
        }

        private void OnSearch(string query)
        {
            query = (query ?? "").Trim().ToLower();

            if (string.IsNullOrWhiteSpace(query))
            {
                _view.RenderReports(_all);
                return;
            }

            var filtered = _all.Where(x =>
                (x.Title ?? "").ToLower().Contains(query) ||
                (x.SubTitle ?? "").ToLower().Contains(query) ||
                (x.BadgeText ?? "").ToLower().Contains(query)
            ).ToList();

            _view.RenderReports(filtered);
        }
    }
}
