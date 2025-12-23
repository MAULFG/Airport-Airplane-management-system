partial class Main1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // Main1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1197, 644);
        Margin = new Padding(3, 2, 3, 2);
        Name = "Main1";
        Text = "Airport Management System";
        Load += Main1_Load;
        ResumeLayout(false);
    }
}
