using System;
using System.Drawing;
using System.Windows.Forms;
using VectorLibrary;

namespace Protractor
{
    partial class FormInputDialog
{
    private System.ComponentModel.IContainer components = null;
    private Label labelPrompt;
    private TextBox textBoxValue;
    private Button buttonOK;
    private Button buttonCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.labelPrompt = new Label();
        this.textBoxValue = new TextBox();
        this.buttonOK = new Button();
        this.buttonCancel = new Button();
        this.SuspendLayout();
        
        // labelPrompt
        this.labelPrompt.AutoSize = true;
        this.labelPrompt.Location = new Point(12, 15);
        this.labelPrompt.Name = "labelPrompt";
        this.labelPrompt.Size = new Size(200, 13);
        this.labelPrompt.TabIndex = 0;
        this.labelPrompt.Font = new System.Drawing.Font("Tahoma", 9F);
        
        // textBoxValue
        this.textBoxValue.Location = new Point(12, 35);
        this.textBoxValue.Name = "textBoxValue";
        this.textBoxValue.Size = new Size(260, 30);
        this.textBoxValue.TabIndex = 1;
        this.textBoxValue.Font = new System.Drawing.Font("Tahoma", 9F);
        this.textBoxValue.KeyPress += new KeyPressEventHandler(this.textBoxValue_KeyPress);
        
        // btnOK
        this.buttonOK.Location = new Point(116, 70);
        this.buttonOK.Name = "buttonOK";
        this.buttonOK.Size = new Size(75, 30);
        this.buttonOK.TabIndex = 2;
        this.buttonOK.Text = "OK";
        this.buttonOK.UseVisualStyleBackColor = true;
        this.buttonOK.Font = new System.Drawing.Font("Tahoma", 9F);
        this.buttonOK.Click += new EventHandler(this.buttonOK_Click); 
        
        // btnCancel
        this.buttonCancel.Location = new Point(197, 70);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new Size(75, 30);
        this.buttonCancel.TabIndex = 3;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9F);
        this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
        
        // InputDialog
        this.AcceptButton = this.buttonOK;
        this.CancelButton = this.buttonCancel;
        this.ClientSize = new Size(284, 105);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.buttonOK);
        this.Controls.Add(this.textBoxValue);
        this.Controls.Add(this.labelPrompt);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
}