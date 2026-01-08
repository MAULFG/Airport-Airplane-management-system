partial class Main1
{
    private System.ComponentModel.IContainer components = null;

    // Cleans up any resources being used
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    // Initializes all components of the form
    private void InitializeComponent()
    {
        SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1264, 681);
        Margin = new Padding(3, 2, 3, 2);
        Name = "Main1";
        Text = "Airport Management System";
        Load += Main1_Load;

        ResumeLayout(false);
    }
}
