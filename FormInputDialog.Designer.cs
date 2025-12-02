using System;
using System.Drawing;
using System.Windows.Forms;
using VectorLibrary;

namespace Protractor
{
    partial class FormInputDialog
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.labelPrompt = new System.Windows.Forms.Label();
        this.textBoxValue = new System.Windows.Forms.TextBox();
        this.buttonOK = new System.Windows.Forms.Button();
        this.buttonCancel = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // labelPrompt
        // 
        this.labelPrompt.AutoSize = true;
        this.labelPrompt.Font = new System.Drawing.Font("Tahoma", 9F);
        this.labelPrompt.Location = new System.Drawing.Point(12, 15);
        this.labelPrompt.Name = "labelPrompt";
        this.labelPrompt.Size = new System.Drawing.Size(0, 18);
        this.labelPrompt.TabIndex = 0;
        // 
        // textBoxValue
        // 
        this.textBoxValue.Font = new System.Drawing.Font("Tahoma", 9F);
        this.textBoxValue.Location = new System.Drawing.Point(12, 35);
        this.textBoxValue.Name = "textBoxValue";
        this.textBoxValue.Size = new System.Drawing.Size(260, 26);
        this.textBoxValue.TabIndex = 1;
        this.textBoxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxValue_KeyPress);
        // 
        // buttonOK
        // 
        this.buttonOK.Font = new System.Drawing.Font("Tahoma", 9F);
        this.buttonOK.Location = new System.Drawing.Point(116, 70);
        this.buttonOK.Name = "buttonOK";
        this.buttonOK.Size = new System.Drawing.Size(75, 30);
        this.buttonOK.TabIndex = 2;
        this.buttonOK.Text = "OK";
        this.buttonOK.UseVisualStyleBackColor = true;
        this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
        // 
        // buttonCancel
        // 
        this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9F);
        this.buttonCancel.Location = new System.Drawing.Point(197, 70);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(75, 30);
        this.buttonCancel.TabIndex = 3;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        // 
        // FormInputDialog
        // 
        this.AcceptButton = this.buttonOK;
        this.CancelButton = this.buttonCancel;
        this.ClientSize = new System.Drawing.Size(284, 105);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.buttonOK);
        this.Controls.Add(this.textBoxValue);
        this.Controls.Add(this.labelPrompt);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FormInputDialog";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Label labelPrompt;
    private System.Windows.Forms.TextBox textBoxValue;
}
}