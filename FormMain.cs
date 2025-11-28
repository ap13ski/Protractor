/*
This application measures angles between objects relative to the origin
of the angular grid. It uses the Graphics.DrawLine() method from the
System.Drawing namespace for rendering support vector lines.
Vector math operations are implemented in the Vector class (VectorLibrary.cs).
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using VectorLibrary;

namespace Protractor
{
    public partial class FormMain : Form
    {
        // The graphics object is used with DrawLine() to render
        // the current state of support vectors v1 and v2.
        private Graphics graphics;

        // Length of the vector
        private const double LENGTH = 205;
        
        // Max symbols count
        private const int SYMBOLS_SHORT = 6;    // Labels
        private const int SYMBOLS_FULL = 12;    // Clipboard, Tooltips

        // The object v1 represents the blue line.
        private Vector v1 = new Vector(0, 0, LENGTH, 0, 1);

        // The object v2 represents the red line.
        private Vector v2 = new Vector(0, 0, LENGTH, 0, 1);

        // Flags whether the left mouse button (LMB) is pressed.
        private bool isDownLMB = false;

        // Flags whether the right mouse button (RMB) is pressed.
        private bool isDownRMB = false;

        // Flags whether the blue line is visible, synchronized with
        // checkBoxShowLineBlue.Checked property.
        private bool isBlueLine = false;

        // Flags whether the red line is visible, synchronized with
        // checkBoxShowLineRed.Checked property.
        private bool isRedLine = false;

        // Flags whether angles are displayed in degrees-minutes-seconds
        // format, synchronized with menuToolAngular.Checked and
        // menuToolDecimal.Checked properties.
        private bool isAngular = true;
        
        // Flags whether the Lock Delta Angle mode is active, synchronized
        // with checkBoxLockDelta.Checked property.
        private bool isDeltaLocked = false;

        // Pen(Color.FromArgb(0, 0, 0));
        private Pen pen_darkblue = new Pen(Color.DarkBlue);
        private Pen pen_blue = new Pen(Color.Blue);
        private Pen pen_darkred = new Pen(Color.DarkRed);
        private Pen pen_red = new Pen(Color.Red);

        public FormMain()
        {
            InitializeComponent();

            // Keyboard event handler in Form1_KeyDown() method
            KeyPreview = true;

            // Mouse event handler in this_MouseWheel() method
            MouseWheel += new MouseEventHandler(this_MouseWheel);
            
            // Hardcodes component sizes and positions in the form
            // constructor to prevent incorrect scaling by the operating
            // system (100%, 125%, 150%).
            // If you know a better approach for handling DPI scaling,
            // please contact me at ap13ski@gmail.com
            SetFormProperties();

            ShowLines();

            UpdateTooltipOpacity();
            UpdateTooltipAngles();
        }
        
        private void ShowLines()
        {
            SetVectorAngle(v1, 60);
            SetVectorAngle(v2, 45);
            checkBoxShowLineBlue.Checked = !checkBoxShowLineBlue.Checked;
            checkBoxShowLineRed.Checked = !checkBoxShowLineRed.Checked;
        }
        
        // Since the angular grid PNG image is the pictureBox.BackgroundImage,
        // this method creates a new Bitmap for the graphics object,
        // effectively clearing the drawing area of previously rendered lines.
        private void LoadScreen()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);
        }
        
        // To improve visual appearance, draws a line by rendering two lines
        // offset by one pixel on the X-axis.
        // An additional 1px adjustment aligns the line origin precisely with
        // the coordinate origin, accounting for the 500x500 form size where
        // the true origin is between pixels.
        // Converts vector coordinates to pictureBox coordinates, as
        // the control's origin (0,0) is at the top-left corner.
        private void DrawLine(Vector v, Pen pen_1, Pen pen_2)
        {
            graphics.DrawLine(
                pen_2,
                ConvertCoordsXtoX(v.X1) - 1,
                ConvertCoordsYtoY(v.Y1) - 1,
                ConvertCoordsXtoX(v.X2) - 1,
                ConvertCoordsYtoY(v.Y2) - 1);
            graphics.DrawLine(
                pen_1,
                ConvertCoordsXtoX(v.X1) - 2,
                ConvertCoordsYtoY(v.Y1) - 1,
                ConvertCoordsXtoX(v.X2) - 2,
                ConvertCoordsYtoY(v.Y2) - 1);
        }
        
        private void RedrawLines()
        {
            LoadScreen();

            if (isBlueLine && !isRedLine)
            { DrawLine(v1, pen_darkblue, pen_blue); }
            
            if (isRedLine && !isBlueLine)
            { DrawLine(v2, pen_darkred, pen_red); }
            
            if (isBlueLine && isRedLine)
            {
                DrawLine(v1, pen_darkblue, pen_blue);
                DrawLine(v2, pen_darkred, pen_red);
            }

            UpdateTooltipAngles();
            pictureBox.Refresh();
            UpdateLabelAngles();
        }
        
        private void UpdateTooltipOpacity()
        {
            string str_inc = string.Format("Current opacity: {0}%\n" +
                                           "Increase opacity (Mouse Wheel Up)",
                                            Convert.ToString(Opacity * 100));
            string str_dec = string.Format("Current opacity: {0}%\n" +
                                           "Decrease opacity (Mouse Wheel Down)",
                                            Convert.ToString(Opacity * 100));
            toolTipMain.SetToolTip(buttonOpacityIncrease, str_inc);
            toolTipMain.SetToolTip(buttonOpacityDecrease, str_dec);
        }

        private void UpdateTooltipAngles()
        {
            string str_blue = string.Format("Blue angle value: {0}\n" +
                                            "Copy to the clipboard (X)",
                                            GetVectorString(v1, SYMBOLS_FULL));
            string str_red = string.Format("Red angle value: {0}\n" +
                                           "Copy to the clipboard (C)",
                                            GetVectorString(v2, SYMBOLS_FULL));
            string str_delta = string.Format("Delta angle value: {0}\n" +
                                             "Copy to the clipboard (V)", 
                                            GetDeltaString(v1, v2, SYMBOLS_FULL));
            
            toolTipMain.SetToolTip(buttonCopyToClipboardBlue, str_blue);
            toolTipMain.SetToolTip(buttonCopyToClipboardRed, str_red);
            toolTipMain.SetToolTip(buttonCopyToClipboardDelta, str_delta);
        }

        private void UpdateLabelAngles()
        {
            string str_blue = "Blue: " + GetVectorString(v1, SYMBOLS_SHORT);
            string str_red = "Red: " + GetVectorString(v2, SYMBOLS_SHORT);
            string str_delta = "Delta: " + GetDeltaString(v1, v2, SYMBOLS_SHORT);
            
            if (isBlueLine && !isRedLine)
                { statusLabelBlue.Text = str_blue; }
            
            if (isRedLine && !isBlueLine)
                { statusLabelRed.Text = str_red; }
            
            if (isBlueLine && isRedLine)
            {
                statusLabelBlue.Text = str_blue;
                statusLabelRed.Text = str_red;
                statusLabelDelta.Text = str_delta;
            }
        }

        private void OpacityUp()
        {
            double step = 0.1;
            Opacity += step;
            UpdateTooltipOpacity();
        }

        // Opacity value is limited to prevent the form from becoming
        // completely invisible.
        private void OpacityDown()
        {
            double min_value = 0.4;
            if (Opacity > min_value)
            {
                double step = 0.1;
                Opacity -= step;
                UpdateTooltipOpacity();
            }
        }

        private void this_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                { OpacityUp(); }
            else
                { OpacityDown(); }
        }

        private void CopyToClipboardVector(Vector v)
        {
            Clipboard.SetText(GetVectorString(v, SYMBOLS_FULL));
        }

        private void CopyToClipboardDelta(Vector v1, Vector v2)
        {
            Clipboard.SetText(GetDeltaString(v1, v2, SYMBOLS_FULL));
        }

        private void ToggleDeltaLocked()
        {
            isDeltaLocked = !isDeltaLocked;
            UpdateLabelAngles();
        }

        private void ShowLineBlue()
        {
            if (checkBoxShowLineBlue.Checked)
            {
                isBlueLine = true;
                statusLabelBlue.Visible = true;
                if (isRedLine)
                    { statusLabelDelta.Visible = true; }
                toolSetBlueAngle.Enabled = true;
            }
            else
            {
                isBlueLine = false;
                LoadScreen();
                UpdateLabelAngles();
                statusLabelBlue.Visible = false;
                statusLabelDelta.Visible = false;
                toolSetBlueAngle.Enabled = false;
            }
            RedrawLines();
        }

        private void ShowLineRed()
        {
            if (checkBoxShowLineRed.Checked)
            {
                isRedLine = true;
                statusLabelRed.Visible = true;
                if (isBlueLine) 
                    { statusLabelDelta.Visible = true; }
                toolSetRedAngle.Enabled = true;
            }
            else
            {
                isRedLine = false;
                LoadScreen();
                UpdateLabelAngles();
                statusLabelRed.Visible = false;
                statusLabelDelta.Visible = false;
                toolSetRedAngle.Enabled = false;
            }
            RedrawLines();
        }
        
        private string ConvertToAngularString(double angle)
        {
            double part_integer = Math.Truncate(angle);
            double part_decimal = angle - Math.Truncate(angle);
            double m = Math.Truncate(part_decimal * 60);
            double s = Math.Truncate(part_decimal * 3600) - Math.Truncate(m) * 60;

            // \u0022 - Unicode symbol "
            string str_result = string.Format("{0}\u00b0 {1}' {2}\u0022", part_integer, m, s);
            
            return str_result;    
        }

        private string GetVectorString(Vector v, int digits)
        {
            string str_result;

            if (isAngular)
            {
                string str_angle = ConvertToAngularString(v.A);
                str_result = string.Format("{0}", str_angle);
            }
            else
            {
                string str_angle = Convert.ToString(Math.Round(v.A, digits));
                str_result = string.Format("{0}\u00b0", str_angle);
            }
            
            return str_result;
        }

        private string GetDeltaString(Vector v1, Vector v2, int digits)
        {
            string str_result;

            if (isAngular)
            {
                double delta = GetDeltaRounded(v1.A, v2.A, digits);
                string str_delta = ConvertToAngularString(delta);
                str_result = str_delta;
            }
            else
            {
                double delta = GetDeltaRounded(v1.A, v2.A, digits);
                string str_delta = Convert.ToString(delta);
                str_result = str_delta + "\u00b0";
            }
            
            return str_result;
        }

        private int ConvertCoordsXtoX(double x)
        {
            try
            {
                // The unsafe type conversion requires exception processing.
                int x_int = Convert.ToInt32(x);
                int output = Convert.ToInt32(x_int + pictureBox.Width / 2);
                return output;
            }
            catch (Exception) { return 0; }
        }

        private int ConvertCoordsYtoY(double y)
        {
            try
            {
                // The unsafe type conversion requires exception processing.
                int y_int = Convert.ToInt32(y);
                int output = Convert.ToInt32(-y_int + pictureBox.Height / 2);
                return output;
            }
            catch (Exception) { return 0; }
        }

        private void RefreshVector(Vector v, object sender, MouseEventArgs e)
        {
            UpdateVectors(v, sender, e);
            RedrawLines();
        }

        private void UpdateVectors(Vector v, object sender, MouseEventArgs e)
        {
            int new_x = ((e.X) - pictureBox.Width / 2);
            int new_y = -((e.Y) - pictureBox.Height / 2);
            double new_angle = Vector.GetAngleByPoints(0, 0, new_x, new_y);

            if (isDeltaLocked)
            {
                double old_angle = v.A;
                double diff_angle = new_angle - old_angle;

                if (v == v1)
                    { v2.SetVectorByAngle(0, 0, LENGTH, RecalculateAngle(v2.A + diff_angle)); }

                if (v == v2)
                    { v1.SetVectorByAngle(0, 0, LENGTH, RecalculateAngle(v1.A + diff_angle)); }
            }

            v.SetVectorByAngle(0, 0, LENGTH, RecalculateAngle(new_angle));
        }

        /*
        Some key presses, such as the TAB, RETURN, ESC, and arrow keys, are typically
        ignored by some controls because they are not considered input key presses.
        For example, by default, a Button control ignores the arrow keys.
        Pressing the arrow keys typically causes the focus to move to the previous
        or next control. The arrow keys are considered navigation keys and pressing
        these keys typically do not raise the KeyDown event for a Button.
        However, pressing the arrow keys for a Button does raise the PreviewKeyDown event.
        By handling the PreviewKeyDown event for a Button and setting the IsInputKey
        property to true, you can raise the KeyDown event when the arrow keys are pressed.
        However, if you handle the arrow keys, the focus will no longer move
        to the previous or next control.
        
        See MSDN Control.PreviewKeyDown Event 
        */
        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void FormMoveCenter()
        {
            Screen screen = Screen.FromControl(this);
    
            this.Left = (screen.Bounds.Width - this.Width) / 2;
            this.Top = (screen.Bounds.Height - this.Height) / 2;
        }
        
        private void FormMoveDirection(string str_dir, int value)
        {
            if (str_dir == "UP")
                { this.Top -= value; }
            
            if (str_dir == "DOWN")
                { this.Top += value; }
            
            if (str_dir == "LEFT")
                { this.Left -= value; }
            
            if (str_dir == "RIGHT")
                { this.Left += value; }
        }
        
        // This provides a reliable solution to prevent the issue of window movement         
        // via arrow keys becoming unresponsive when the form's TopMost property is set to true.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || 
                keyData == Keys.Left || keyData == Keys.Right)
            {
                int value = 1;
                
                if (keyData == Keys.Up)
                    { FormMoveDirection("UP", value); }
                if (keyData == Keys.Down)
                    { FormMoveDirection("DOWN", value); }
                if (keyData == Keys.Left)
                    { FormMoveDirection("LEFT", value); }
                if (keyData == Keys.Right)
                    { FormMoveDirection("RIGHT", value); }

                return true;
            }
    
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T)
            {
                checkBoxAlwaysOnTop.Checked = !checkBoxAlwaysOnTop.Checked;
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.X)
            {
                CopyToClipboardVector(v1);
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.C)
            {
                CopyToClipboardVector(v2);
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.V)
            {
                CopyToClipboardDelta(v1, v2);
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.L)
            {
                checkBoxLockDelta.Checked = !checkBoxLockDelta.Checked;
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F1)
            {
                ShowHelp();
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.Home)
            {
                FormMoveCenter();
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                int value = 1;

                if (e.Control && !e.Alt)
                    { value = 10; }
                if (!e.Control && e.Alt)
                    { value = 50; }

                if (e.KeyCode == Keys.Up)
                    { FormMoveDirection("UP", value); }
                if (e.KeyCode == Keys.Down)
                    { FormMoveDirection("DOWN", value); }
                if (e.KeyCode == Keys.Left)
                    { FormMoveDirection("LEFT", value); }
                if (e.KeyCode == Keys.Right)
                    { FormMoveDirection("RIGHT", value); }

                e.Handled = true;
            }

            if (e.KeyCode == Keys.D1 && !e.Control)
            {
                checkBoxShowLineBlue.Checked = !checkBoxShowLineBlue.Checked;
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.D2 && !e.Control)
            {
                checkBoxShowLineRed.Checked = !checkBoxShowLineRed.Checked;
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.D1 && e.Control)
            {
                SetAngleArbitrary("BLUE");
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.D2 && e.Control)
            {
                SetAngleArbitrary("RED");
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.U)
            {
                menuToolAngular.Checked = !menuToolAngular.Checked;
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.A)
            {
                menuToolAngular.Checked = true;
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.D)
            {
                menuToolDecimal.Checked = true;
                e.Handled = true;
            }
        }

        private double GetDeltaRounded(double a1, double a2, int digits)
        {
            double delta = Math.Round(Math.Abs(a1 - a2), digits);
            
            if (delta > 180)
                { return 360 - delta; }
            else 
                { return delta; }
        }

        private double RecalculateAngle(double angle)
        {
            if (angle > 360)
                { return angle - 360; }
            if (angle < 0)
                { return angle + 360; }

            return angle;
        }
        
        private double GetDelta(Vector v1, Vector v2)
        {
            double delta = Math.Abs(v1.A - v2.A);

            if (v1.A >= v2.A)
                { return delta; }
            else
                { return -delta;}
        }
        
        private void SetDecimal()
        {
            if (isAngular)
            {
                isAngular = false;
                menuToolAngular.Checked = false;
                menuToolDecimal.Checked = true;
                UpdateLabelAngles();
                UpdateTooltipAngles();
            }
        }
        
        private void SetAngular()
        {
            if (isAngular == false)
            {
                isAngular = true;
                menuToolAngular.Checked = true;
                menuToolDecimal.Checked = false;
                UpdateLabelAngles();
                UpdateTooltipAngles();
            }
        }

        private void SetVectorAngle(Vector v, double angle)
        {
            double delta = GetDelta(v1, v2);
            
            v.SetVectorByAngle(0, 0, LENGTH, angle);
            
            if (isDeltaLocked)
            {
                if (v == v1)
                {
                    double new_angle = RecalculateAngle(angle - delta);
                    v2.SetVectorByAngle(0, 0, LENGTH, new_angle);
                }

                if (v == v2)
                {
                    double new_angle = RecalculateAngle(angle + delta);
                    v1.SetVectorByAngle(0, 0, LENGTH, new_angle);
                }
            }
            
            RedrawLines();
        }
        
        private void SetAngleArbitrary(string color)
        {
            string title = "Arbitrary angle value";
            string prompt = string.Format("Enter the {0} angle value:", color);
            double default_value = 0.0;
            
            using (var dialog = new FormInputDialog(title, prompt, default_value))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (color == "BLUE")
                        { SetVectorAngle(v1, dialog.Value); }
                    if (color == "RED")
                        { SetVectorAngle(v2, dialog.Value); }
                }
            }
        }
        
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                { isDownLMB = false; }
            if (e.Button == MouseButtons.Right)
                { isDownRMB = false; }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                { isDownLMB = true; }
            if (e.Button == MouseButtons.Right)
                { isDownRMB = true; }

            if (isBlueLine && e.Button == MouseButtons.Left)
                { RefreshVector(v1, sender, e); }
            if (isRedLine && e.Button == MouseButtons.Right)
                { RefreshVector(v2, sender, e); }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDownLMB && e.Button == MouseButtons.Left && isBlueLine)
                { RefreshVector(v1, sender, e); }
            if (isDownRMB && e.Button == MouseButtons.Right && isRedLine)
                { RefreshVector(v2, sender, e); }
        }
        
        private void checkBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAlwaysOnTop.Checked)
                { TopMost = true; }
            else
                { TopMost = false; }
        }
        
        private void buttonOpacityDecrease_Click(object sender, EventArgs e)
        {
            OpacityDown();
        }
        
        private void buttonOpacityIncrease_Click(object sender, EventArgs e)
        {
            OpacityUp();
        }
        
        private void checkBoxLockDelta_CheckedChanged(object sender, EventArgs e)
        {
            ToggleDeltaLocked();
        }
        
        private void checkBoxShowLineBlue_CheckedChanged(object sender, EventArgs e)
        {
            ShowLineBlue();
        }

        private void checkBoxShowLineRed_CheckedChanged(object sender, EventArgs e)
        {
            ShowLineRed();
        }

        private void buttonCopyToClipboardBlue_Click(object sender, EventArgs e)
        {
            CopyToClipboardVector(v1);
        }

        private void buttonCopyToClipboardRed_Click(object sender, EventArgs e)
        {
            CopyToClipboardVector(v2);
        }

        private void buttonCopyToClipboardDelta_Click(object sender, EventArgs e)
        {
            CopyToClipboardDelta(v1, v2);
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }
        
        private void menuToolDecimal_CheckedChanged(object sender, EventArgs e)
        {
            if (menuToolDecimal.Checked)
                { SetDecimal(); }
            else 
                { SetAngular(); }
        }
        
        private void menuToolAngular_CheckedChanged(object sender, EventArgs e)
        {
            if (menuToolAngular.Checked)
                { SetAngular(); }
            else 
                { SetDecimal(); }
        }

        private void toolSetAngleBlue0_Click(object sender, EventArgs e) { SetVectorAngle(v1, 0); }
        private void toolSetAngleBlue30_Click(object sender, EventArgs e) { SetVectorAngle(v1, 30); }
        private void toolSetAngleBlue45_Click(object sender, EventArgs e) { SetVectorAngle(v1, 45); }
        private void toolSetAngleBlue60_Click(object sender, EventArgs e) { SetVectorAngle(v1, 60); }
        private void toolSetAngleBlue90_Click(object sender, EventArgs e) { SetVectorAngle(v1, 90); }
        private void toolSetAngleBlue120_Click(object sender, EventArgs e) { SetVectorAngle(v1, 120); }
        private void toolSetAngleBlue180_Click(object sender, EventArgs e) { SetVectorAngle(v1, 180); }
        private void toolSetAngleBlue270_Click(object sender, EventArgs e) { SetVectorAngle(v1, 270); }
        private void toolSetAngleBlueArbitrary_Click(object sender, EventArgs e) { SetAngleArbitrary("BLUE"); }

        private void toolSetAngleRed0_Click(object sender, EventArgs e) { SetVectorAngle(v2, 0); }
        private void toolSetAngleRed30_Click(object sender, EventArgs e) { SetVectorAngle(v2, 30); }
        private void toolSetAngleRed45_Click(object sender, EventArgs e) { SetVectorAngle(v2, 45); }
        private void toolSetAngleRed60_Click(object sender, EventArgs e) { SetVectorAngle(v2, 60); }
        private void toolSetAngleRed90_Click(object sender, EventArgs e) { SetVectorAngle(v2, 90); }
        private void toolSetAngleRed120_Click(object sender, EventArgs e) { SetVectorAngle(v2, 120); }
        private void toolSetAngleRed180_Click(object sender, EventArgs e) { SetVectorAngle(v2, 180); }
        private void toolSetAngleRed270_Click(object sender, EventArgs e) { SetVectorAngle(v2, 270); }
        private void toolSetAngleRedArbitrary_Click(object sender, EventArgs e) { SetAngleArbitrary("RED"); }

        private void ShowHelp()
        {
            string msg_text = "Protractor 1.2.1 64-bit\n" +
                              "Created by ap13ski\n" +
                              "https://github.com/ap13ski\n" +
                              "ap13ski@gmail.com\n" +
                              "Special thanks to Eduardo Steffler Werner.\n\n\n" +
                              "Press [F1] to show this information window.\n\n" +
                              "Activate the Blue support line (BSL) and/or the Red support line (RSL)" +
                              " using their corresponding switches in the lower-left corner" +
                              " of the window or press [1] or [2]." +
                              " The angle values will be displayed on the status bar.\n\n" +
                              "To position the BSL (the RSL), click and hold the left (right) mouse button" +
                              " anywhere in the window. For greater precision, you can release" +
                              " the mouse button *anywhere* on the screen, including the area outside" +
                              " the window.\n\n" +
                              "Use the switch in the lower-left corner or press [L] to toggle the \u0022Lock" +
                              " Delta Angle\u0022 mode.\n\n" +
                              "Adjust the window opacity using the mouse wheel. Press [T] to toggle" +
                              " \u0022Always on top\u0022 mode.\n\nUse the gear icon button" +
                              " on the status bar or press [U] to toggle the angle value units." +
                              " Press [A] to set angular units (d\u00b0 m' s\u0022), press [D] to set" +
                              " decimal units (d,nnn\u00b0). This menu also allows you to set" +
                              " fixed angle values of the support lines.\n" +
                              "To set an arbitrary angle value, press [Ctrl+1] or [Ctrl+2].\n\n" +
                              "Copy the angle values using the buttons in the lower-left corner of the window," +
                              " located below the BSL and the RSL switches, or with [X], [C], [V].\n\n" +
                              "Move the window with the arrow keys [\u2191], [\u2193], [\u2190], [\u2192] by 1px." +
                              " Hold [Ctrl] or [Alt] to move the window by 10px or 50px, respectively." +
                              " Press [Home] to center the window on the screen.\n\n";
            
            string msg_caption = "Information";
            var msg_buttons = MessageBoxButtons.OK;
            var msg_icon = MessageBoxIcon.Information;

            MessageBox.Show(this, msg_text, msg_caption, msg_buttons, msg_icon);
        }

        // Hardcodes component sizes and positions in the form
        // constructor to prevent incorrect scaling by the operating
        // system (100%, 125%, 150%).
        // If you know a better approach for handling DPI scaling, please
        // contact me at ap13ski@gmail.com
        private void SetFormProperties()
        {
            statusStripBar.Size = new System.Drawing.Size(240, 24);
            statusStripBar.BackColor = Color.FromArgb(255, 212, 208, 200);

            ClientSize = new System.Drawing.Size(500, 500 + statusStripBar.Size.Height + 4);
            BackColor = Color.FromArgb(255, 212, 208, 200);

            pictureBox.Location = new System.Drawing.Point(0, 0);
            pictureBox.Size = new System.Drawing.Size(500, 500);
            
            System.Drawing.Size button_size = new System.Drawing.Size(30, 30);

            // top left block
            buttonOpacityIncrease.Size = button_size;
            buttonOpacityIncrease.Location = new System.Drawing.Point(72, 4);

            buttonOpacityDecrease.Size = button_size;
            buttonOpacityDecrease.Location = new System.Drawing.Point(38, 4);

            checkBoxAlwaysOnTop.Size = button_size;
            checkBoxAlwaysOnTop.Location = new System.Drawing.Point(4, 4);
            
            // top right block
            buttonHelp.Size = button_size;
            buttonHelp.Location = new System.Drawing.Point(466, 4);

            // bottom left block
            buttonCopyToClipboardBlue.Size = button_size;
            buttonCopyToClipboardBlue.Location = new System.Drawing.Point(4, 468);

            buttonCopyToClipboardRed.Size = button_size;
            buttonCopyToClipboardRed.Location = new System.Drawing.Point(38, 468);

            buttonCopyToClipboardDelta.Size = button_size;
            buttonCopyToClipboardDelta.Location = new System.Drawing.Point(72, 468);

            checkBoxLockDelta.Size = button_size;
            checkBoxLockDelta.Location = new System.Drawing.Point(4, 400);
            
            checkBoxShowLineBlue.Size = button_size;
            checkBoxShowLineBlue.Location = new System.Drawing.Point(4, 434);

            checkBoxShowLineRed.Size = button_size;
            checkBoxShowLineRed.Location = new System.Drawing.Point(38, 434);
            
            System.Drawing.Font status_font = new System.Drawing.Font("Tahoma", 10F);
            string str_empty = "";

            // bottom block
            statusLabelBlue.ForeColor = System.Drawing.Color.DarkBlue;
            statusLabelBlue.Font = status_font;
            statusLabelBlue.Text = str_empty;

            statusLabelRed.ForeColor = System.Drawing.Color.DarkRed;
            statusLabelRed.Font = status_font;
            statusLabelRed.Text = str_empty;

            statusLabelDelta.ForeColor = System.Drawing.Color.Black;
            statusLabelDelta.Font = status_font;
            statusLabelDelta.Text = str_empty;

            menuToolAngular.Text = "Format: d\u00b0 m' s\u0022 (A)"; // \u0022 unicode symbol "
            menuToolDecimal.Text = "Format: d,nnn\u00b0 (D)"; // \u00b0 unicode symbol °
        }
    }
}