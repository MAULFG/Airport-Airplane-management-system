using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Controls
{
    public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
    {
        public DoubleBufferedFlowLayoutPanel()
        {
            // DO NOT enable UserPaint for a container
            DoubleBuffered = true;

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);

            UpdateStyles();
        }
    }
}