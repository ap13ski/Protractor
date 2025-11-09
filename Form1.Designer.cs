using System.Windows.Forms;

namespace Protractor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonOpacityIncrease = new System.Windows.Forms.Button();
            this.buttonOpacityDecrease = new System.Windows.Forms.Button();
            this.checkBoxAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.checkBoxShowLineBlue = new System.Windows.Forms.CheckBox();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxShowLineRed = new System.Windows.Forms.CheckBox();
            this.buttonCopyToClipboardRed = new System.Windows.Forms.Button();
            this.buttonCopyToClipboardDelta = new System.Windows.Forms.Button();
            this.buttonCopyToClipboardBlue = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusToolStrip = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolSetBlueAngle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetAngleBlue0 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetAngleBlue90 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetAngleBlue180 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetAngleBlue270 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetRedAngle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetAngleRed0 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetAngleRed90 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetAngleRed180 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetAngleRed270 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuToolAngular = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolDecimal = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLabelBlue = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelRed = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDelta = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpacityIncrease
            // 
            this.buttonOpacityIncrease.FlatAppearance.BorderSize = 0;
            this.buttonOpacityIncrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpacityIncrease.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.buttonOpacityIncrease.Image = ((System.Drawing.Image) (resources.GetObject("buttonOpacityIncrease.Image")));
            this.buttonOpacityIncrease.Location = new System.Drawing.Point(3, 3);
            this.buttonOpacityIncrease.Name = "buttonOpacityIncrease";
            this.buttonOpacityIncrease.Size = new System.Drawing.Size(20, 20);
            this.buttonOpacityIncrease.TabIndex = 0;
            this.buttonOpacityIncrease.TabStop = false;
            this.buttonOpacityIncrease.UseVisualStyleBackColor = true;
            this.buttonOpacityIncrease.Click += new System.EventHandler(this.buttonOpacityIncrease_Click);
            // 
            // buttonOpacityDecrease
            // 
            this.buttonOpacityDecrease.FlatAppearance.BorderSize = 0;
            this.buttonOpacityDecrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpacityDecrease.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.buttonOpacityDecrease.Image = ((System.Drawing.Image) (resources.GetObject("buttonOpacityDecrease.Image")));
            this.buttonOpacityDecrease.Location = new System.Drawing.Point(29, 3);
            this.buttonOpacityDecrease.Name = "buttonOpacityDecrease";
            this.buttonOpacityDecrease.Size = new System.Drawing.Size(20, 20);
            this.buttonOpacityDecrease.TabIndex = 1;
            this.buttonOpacityDecrease.TabStop = false;
            this.buttonOpacityDecrease.UseVisualStyleBackColor = true;
            this.buttonOpacityDecrease.Click += new System.EventHandler(this.buttonOpacityDecrease_Click);
            // 
            // checkBoxAlwaysOnTop
            // 
            this.checkBoxAlwaysOnTop.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAlwaysOnTop.FlatAppearance.BorderSize = 0;
            this.checkBoxAlwaysOnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAlwaysOnTop.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.checkBoxAlwaysOnTop.Image = ((System.Drawing.Image) (resources.GetObject("checkBoxAlwaysOnTop.Image")));
            this.checkBoxAlwaysOnTop.Location = new System.Drawing.Point(55, 3);
            this.checkBoxAlwaysOnTop.Name = "checkBoxAlwaysOnTop";
            this.checkBoxAlwaysOnTop.Size = new System.Drawing.Size(20, 20);
            this.checkBoxAlwaysOnTop.TabIndex = 3;
            this.checkBoxAlwaysOnTop.TabStop = false;
            this.toolTipMain.SetToolTip(this.checkBoxAlwaysOnTop, "Toggle \u0022Always on top\u0022 mode (T)");
            this.checkBoxAlwaysOnTop.UseVisualStyleBackColor = true;
            this.checkBoxAlwaysOnTop.CheckedChanged += new System.EventHandler(this.checkBoxAlwaysOnTop_CheckedChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("pictureBox.BackgroundImage")));
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(91, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(118, 112);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // checkBoxShowLineBlue
            // 
            this.checkBoxShowLineBlue.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxShowLineBlue.FlatAppearance.BorderSize = 0;
            this.checkBoxShowLineBlue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShowLineBlue.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.checkBoxShowLineBlue.Image = ((System.Drawing.Image) (resources.GetObject("checkBoxShowLineBlue.Image")));
            this.checkBoxShowLineBlue.Location = new System.Drawing.Point(3, 127);
            this.checkBoxShowLineBlue.Name = "checkBoxShowLineBlue";
            this.checkBoxShowLineBlue.Size = new System.Drawing.Size(20, 20);
            this.checkBoxShowLineBlue.TabIndex = 4;
            this.checkBoxShowLineBlue.TabStop = false;
            this.toolTipMain.SetToolTip(this.checkBoxShowLineBlue, "Show/hide the Blue support line (1):\r\n – Press LMB on the form to draw the line \r\n – Hold" + " LMB on the form and move the cursor to change the angle");
            this.checkBoxShowLineBlue.UseVisualStyleBackColor = true;
            this.checkBoxShowLineBlue.CheckedChanged += new System.EventHandler(this.checkBoxShowLineBlue_CheckedChanged);
            // 
            // toolTipMain
            // 
            this.toolTipMain.AutomaticDelay = 250;
            this.toolTipMain.AutoPopDelay = 8000;
            this.toolTipMain.InitialDelay = 250;
            this.toolTipMain.ReshowDelay = 50;
            // 
            // checkBoxShowLineRed
            // 
            this.checkBoxShowLineRed.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxShowLineRed.FlatAppearance.BorderSize = 0;
            this.checkBoxShowLineRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShowLineRed.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.checkBoxShowLineRed.Image = ((System.Drawing.Image) (resources.GetObject("checkBoxShowLineRed.Image")));
            this.checkBoxShowLineRed.Location = new System.Drawing.Point(29, 127);
            this.checkBoxShowLineRed.Name = "checkBoxShowLineRed";
            this.checkBoxShowLineRed.Size = new System.Drawing.Size(20, 20);
            this.checkBoxShowLineRed.TabIndex = 4;
            this.checkBoxShowLineRed.TabStop = false;
            this.toolTipMain.SetToolTip(this.checkBoxShowLineRed, "Show/hide the Red support line (2):\r\n – Press RMB on the form to draw the line \r\n – Hold " + "RMB on the form and move the cursor to change the angle");
            this.checkBoxShowLineRed.UseVisualStyleBackColor = true;
            this.checkBoxShowLineRed.CheckedChanged += new System.EventHandler(this.checkBoxShowLineRed_CheckedChanged);
            // 
            // buttonCopyToClipboardRed
            // 
            this.buttonCopyToClipboardRed.FlatAppearance.BorderSize = 0;
            this.buttonCopyToClipboardRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopyToClipboardRed.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.buttonCopyToClipboardRed.Image = ((System.Drawing.Image) (resources.GetObject("buttonCopyToClipboardRed.Image")));
            this.buttonCopyToClipboardRed.Location = new System.Drawing.Point(29, 153);
            this.buttonCopyToClipboardRed.Name = "buttonCopyToClipboardRed";
            this.buttonCopyToClipboardRed.Size = new System.Drawing.Size(20, 20);
            this.buttonCopyToClipboardRed.TabIndex = 5;
            this.buttonCopyToClipboardRed.TabStop = false;
            this.buttonCopyToClipboardRed.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboardRed.Click += new System.EventHandler(this.buttonCopyToClipboardRed_Click);
            // 
            // buttonCopyToClipboardDelta
            // 
            this.buttonCopyToClipboardDelta.FlatAppearance.BorderSize = 0;
            this.buttonCopyToClipboardDelta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopyToClipboardDelta.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.buttonCopyToClipboardDelta.Image = ((System.Drawing.Image) (resources.GetObject("buttonCopyToClipboardDelta.Image")));
            this.buttonCopyToClipboardDelta.Location = new System.Drawing.Point(55, 153);
            this.buttonCopyToClipboardDelta.Name = "buttonCopyToClipboardDelta";
            this.buttonCopyToClipboardDelta.Size = new System.Drawing.Size(20, 20);
            this.buttonCopyToClipboardDelta.TabIndex = 6;
            this.buttonCopyToClipboardDelta.TabStop = false;
            this.buttonCopyToClipboardDelta.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboardDelta.Click += new System.EventHandler(this.buttonCopyToClipboardDelta_Click);
            // 
            // buttonCopyToClipboardBlue
            // 
            this.buttonCopyToClipboardBlue.FlatAppearance.BorderSize = 0;
            this.buttonCopyToClipboardBlue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopyToClipboardBlue.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.buttonCopyToClipboardBlue.Image = ((System.Drawing.Image) (resources.GetObject("buttonCopyToClipboardBlue.Image")));
            this.buttonCopyToClipboardBlue.Location = new System.Drawing.Point(3, 153);
            this.buttonCopyToClipboardBlue.Name = "buttonCopyToClipboardBlue";
            this.buttonCopyToClipboardBlue.Size = new System.Drawing.Size(20, 20);
            this.buttonCopyToClipboardBlue.TabIndex = 2;
            this.buttonCopyToClipboardBlue.TabStop = false;
            this.buttonCopyToClipboardBlue.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboardBlue.Click += new System.EventHandler(this.buttonCopyToClipboardBlue_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.statusToolStrip, this.statusLabelBlue, this.statusLabelRed, this.statusLabelDelta});
            this.statusStrip1.Location = new System.Drawing.Point(0, 296);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(350, 27);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusToolStrip
            // 
            this.statusToolStrip.AutoSize = false;
            this.statusToolStrip.AutoToolTip = false;
            this.statusToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolSetBlueAngle, this.toolSetRedAngle, this.toolStripSeparator1, this.menuToolAngular, this.menuToolDecimal});
            this.statusToolStrip.Image = ((System.Drawing.Image) (resources.GetObject("statusToolStrip.Image")));
            this.statusToolStrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.statusToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statusToolStrip.Name = "statusToolStrip";
            this.statusToolStrip.Size = new System.Drawing.Size(34, 25);
            this.statusToolStrip.Text = "toolStripDropDownButton1";
            // 
            // toolSetBlueAngle
            // 
            this.toolSetBlueAngle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolSetAngleBlue0, this.toolSetAngleBlue90, this.toolSetAngleBlue180, this.toolSetAngleBlue270});
            this.toolSetBlueAngle.Enabled = false;
            this.toolSetBlueAngle.Name = "toolSetBlueAngle";
            this.toolSetBlueAngle.Size = new System.Drawing.Size(218, 26);
            this.toolSetBlueAngle.Text = "Set: Blue angle value";
            // 
            // toolSetAngleBlue0
            // 
            this.toolSetAngleBlue0.Name = "toolSetAngleBlue0";
            this.toolSetAngleBlue0.Size = new System.Drawing.Size(113, 26);
            this.toolSetAngleBlue0.Text = "0°";
            this.toolSetAngleBlue0.Click += new System.EventHandler(this.toolSetAngleBlue0_Click);
            // 
            // toolSetAngleBlue90
            // 
            this.toolSetAngleBlue90.Name = "toolSetAngleBlue90";
            this.toolSetAngleBlue90.Size = new System.Drawing.Size(113, 26);
            this.toolSetAngleBlue90.Text = "90°";
            this.toolSetAngleBlue90.Click += new System.EventHandler(this.toolSetAngleBlue90_Click);
            // 
            // toolSetAngleBlue180
            // 
            this.toolSetAngleBlue180.Name = "toolSetAngleBlue180";
            this.toolSetAngleBlue180.Size = new System.Drawing.Size(113, 26);
            this.toolSetAngleBlue180.Text = "180°";
            this.toolSetAngleBlue180.Click += new System.EventHandler(this.toolSetAngleBlue180_Click);
            // 
            // toolSetAngleBlue270
            // 
            this.toolSetAngleBlue270.Name = "toolSetAngleBlue270";
            this.toolSetAngleBlue270.Size = new System.Drawing.Size(113, 26);
            this.toolSetAngleBlue270.Text = "270°";
            this.toolSetAngleBlue270.Click += new System.EventHandler(this.toolSetAngleBlue270_Click);
            // 
            // toolSetRedAngle
            // 
            this.toolSetRedAngle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolSetAngleRed0, this.toolSetAngleRed90, this.toolSetAngleRed180, this.toolSetAngleRed270});
            this.toolSetRedAngle.Enabled = false;
            this.toolSetRedAngle.Name = "toolSetRedAngle";
            this.toolSetRedAngle.Size = new System.Drawing.Size(218, 26);
            this.toolSetRedAngle.Text = "Set: Red angle value";
            // 
            // toolSetAngleRed0
            // 
            this.toolSetAngleRed0.Name = "toolSetAngleRed0";
            this.toolSetAngleRed0.Size = new System.Drawing.Size(113, 26);
            this.toolSetAngleRed0.Text = "0°";
            this.toolSetAngleRed0.Click += new System.EventHandler(this.toolSetAngleRed0_Click);
            // 
            // toolSetAngleRed90
            // 
            this.toolSetAngleRed90.Name = "toolSetAngleRed90";
            this.toolSetAngleRed90.Size = new System.Drawing.Size(113, 26);
            this.toolSetAngleRed90.Text = "90°";
            this.toolSetAngleRed90.Click += new System.EventHandler(this.toolSetAngleRed90_Click);
            // 
            // toolSetAngleRed180
            // 
            this.toolSetAngleRed180.Name = "toolSetAngleRed180";
            this.toolSetAngleRed180.Size = new System.Drawing.Size(113, 26);
            this.toolSetAngleRed180.Text = "180°";
            this.toolSetAngleRed180.Click += new System.EventHandler(this.toolSetAngleRed180_Click);
            // 
            // toolSetAngleRed270
            // 
            this.toolSetAngleRed270.Name = "toolSetAngleRed270";
            this.toolSetAngleRed270.Size = new System.Drawing.Size(113, 26);
            this.toolSetAngleRed270.Text = "270°";
            this.toolSetAngleRed270.Click += new System.EventHandler(this.toolSetAngleRed270_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
            // 
            // menuToolAngular
            // 
            this.menuToolAngular.Checked = true;
            this.menuToolAngular.CheckOnClick = true;
            this.menuToolAngular.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuToolAngular.Name = "menuToolAngular";
            this.menuToolAngular.Size = new System.Drawing.Size(218, 26);
            this.menuToolAngular.Text = "angular";
            this.menuToolAngular.CheckedChanged += new System.EventHandler(this.menuToolAngular_CheckedChanged);
            // 
            // menuToolDecimal
            // 
            this.menuToolDecimal.CheckOnClick = true;
            this.menuToolDecimal.Name = "menuToolDecimal";
            this.menuToolDecimal.Size = new System.Drawing.Size(218, 26);
            this.menuToolDecimal.Text = "decimal";
            this.menuToolDecimal.CheckedChanged += new System.EventHandler(this.menuToolDecimal_CheckedChanged);
            // 
            // statusLabelBlue
            // 
            this.statusLabelBlue.Font = new System.Drawing.Font("Tahoma", 9F);
            this.statusLabelBlue.ForeColor = System.Drawing.Color.DarkBlue;
            this.statusLabelBlue.Name = "statusLabelBlue";
            this.statusLabelBlue.Size = new System.Drawing.Size(35, 22);
            this.statusLabelBlue.Text = "Blue";
            // 
            // statusLabelRed
            // 
            this.statusLabelRed.Name = "statusLabelRed";
            this.statusLabelRed.Size = new System.Drawing.Size(33, 22);
            this.statusLabelRed.Text = "Red";
            // 
            // statusLabelDelta
            // 
            this.statusLabelDelta.Name = "statusLabelDelta";
            this.statusLabelDelta.Size = new System.Drawing.Size(41, 22);
            this.statusLabelDelta.Text = "Delta";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(350, 323);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonCopyToClipboardDelta);
            this.Controls.Add(this.buttonCopyToClipboardRed);
            this.Controls.Add(this.buttonCopyToClipboardBlue);
            this.Controls.Add(this.buttonOpacityIncrease);
            this.Controls.Add(this.buttonOpacityDecrease);
            this.Controls.Add(this.checkBoxShowLineRed);
            this.Controls.Add(this.checkBoxShowLineBlue);
            this.Controls.Add(this.checkBoxAlwaysOnTop);
            this.Controls.Add(this.pictureBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.Text = "Protractor 1.1 (press F1 for help)";
            this.PreviewKeyDown +=new PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button buttonCopyToClipboardBlue;
        private System.Windows.Forms.Button buttonCopyToClipboardDelta;
        private System.Windows.Forms.Button buttonCopyToClipboardRed;
        private System.Windows.Forms.Button buttonOpacityDecrease;
        private System.Windows.Forms.Button buttonOpacityIncrease;
        private System.Windows.Forms.CheckBox checkBoxAlwaysOnTop;
        private System.Windows.Forms.CheckBox checkBoxShowLineBlue;
        private System.Windows.Forms.CheckBox checkBoxShowLineRed;
        private System.Windows.Forms.ToolStripMenuItem menuToolAngular;
        private System.Windows.Forms.ToolStripMenuItem menuToolDecimal;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelBlue;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDelta;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelRed;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton statusToolStrip;
        private System.Windows.Forms.ToolStripMenuItem toolSetAngleBlue0;
        private System.Windows.Forms.ToolStripMenuItem toolSetAngleBlue180;
        private System.Windows.Forms.ToolStripMenuItem toolSetAngleBlue270;
        private System.Windows.Forms.ToolStripMenuItem toolSetAngleBlue90;
        private System.Windows.Forms.ToolStripMenuItem toolSetAngleRed0;
        private System.Windows.Forms.ToolStripMenuItem toolSetAngleRed180;
        private System.Windows.Forms.ToolStripMenuItem toolSetAngleRed270;
        private System.Windows.Forms.ToolStripMenuItem toolSetAngleRed90;
        private System.Windows.Forms.ToolStripMenuItem toolSetBlueAngle;
        private System.Windows.Forms.ToolStripMenuItem toolSetRedAngle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolTip toolTipMain;

        #endregion
    }
}

