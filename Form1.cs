/*
The application allows to measure the angles between
objects relative to the center of the angular grid.
The Graphics.Drawline() method of the System.Drawing
namespace is used for additional graphical drawings.
The library of vector methods is located in Vector 
class in Vector.cs file.
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using VectorLibrary;

namespace Protractor
{
    public partial class Form1 : Form
    {
        /*
        The object graphics is used to call the Drawline() method,
        which represents the current state of vectors v1 and v2 -
        the instances of Vector class from VectorLibrary.cs file.
        */
        private Graphics graphics;

        // Standard length of support vector for current grid
        private const double LENGTH_VISUAL = 205;

        // The object v1 represents the blue line.
        private Vector v1 = new Vector(0, 0, LENGTH_VISUAL, 0, 1);

        // The object v2 represents the red line.
        private Vector v2 = new Vector(0, 0, LENGTH_VISUAL, 0, 1);

        // Represents left mouse button (LMB) pressed state.
        // Variable is used to define a new position of vectors v1
        // depending on the cursor coordinates. 
        private bool isDownLMB = false;

        // Represents right mouse button (RMB) pressed state.
        // Variable is used to define a new position of vectors v2
        // depending on the cursor coordinates. 
        private bool isDownRMB = false;

        // Represents the status of the associated property
        // checkBoxShowLineBlue.Checked
        private bool isBlueLine = false;

        // Represents the status of the associated property
        // checkBoxShowLineRed.Checked
        private bool isRedLine = false;

        // Represents the status of the associated properties
        // menuToolAngular.Checked and menuToolDecimal.Checked
        private bool isAngleInDegrees = true;

        private bool isDeltaAngleLocked = false;

        private Pen pen_darkblue = new Pen(Color.DarkBlue); //Pen(Color.FromArgb(16, 93, 163));
        private Pen pen_blue = new Pen(Color.Blue); //Pen(Color.FromArgb(22, 103, 183));
        private Pen pen_darkred = new Pen(Color.DarkRed); //Pen(Color.FromArgb(190, 0, 0));
        private Pen pen_red = new Pen(Color.Red); //Pen(Color.FromArgb(200, 0, 0));

        public Form1()
        {
            InitializeComponent();

            // Keyboard event handler in Form1_KeyDown() method
            KeyPreview = true;

            // Mouse event handler in this_MouseWheel() method
            MouseWheel += new MouseEventHandler(this_MouseWheel);
            
            /*
            To avoid incorrect scaling of components on the form 
            by the operating system (100%, 125%, 150%) their sizes
            and positions are set hard in the form constructor.     
            */
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

        /*
        A common method to change the hints about the current opacity
        of the form for buttonOpacityIncrease and buttonOpacityDecrease
        buttons using toolTipMain component.
        */
        private void UpdateTooltipOpacity()
        {
            string str_inc = string.Format("Current opacity: {0}%\nIncrease opacity (Mouse Wheel Up)", Convert.ToString(Opacity * 100));
            string str_dec = string.Format("Current opacity: {0}%\nDecrease opacity (Mouse Wheel Down)", Convert.ToString(Opacity * 100));
            toolTipMain.SetToolTip(buttonOpacityIncrease, str_inc);
            toolTipMain.SetToolTip(buttonOpacityDecrease, str_dec);
        }

        /*
        The common method to change the hints about the current values
        of angles of vectors v1 and v2 for buttonCopyToClipboardBlue,
        buttonCopyToClipboardRed and buttonCopyToClipboardDelta using
        toolTipMain component.
        */
        private void UpdateTooltipAngles()
        {
            string str_blue = string.Format("Blue angle value: {0}\nCopy to the clipboard (X)", GetAngleValueVectorString(v1, 12));
            string str_red = string.Format("Red angle value: {0}\nCopy to the clipboard (C)", GetAngleValueVectorString(v2, 12));
            string str_delta = string.Format("Delta angle value: {0}\nCopy to the clipboard (V)", GetAngleValueDeltaString(v1, v2, 12));
            
            toolTipMain.SetToolTip(buttonCopyToClipboardBlue, str_blue);
            toolTipMain.SetToolTip(buttonCopyToClipboardRed, str_red);
            toolTipMain.SetToolTip(buttonCopyToClipboardDelta, str_delta);
        }

        /*
        The angular grid as a PNG-image is loaded to the pictureBox.BackgroundImage.
        A LoadScreen() method loads a new Bitmap to the object graphics,
        which allows to clear the workspace from previously drawn lines.
        */
        private void LoadScreen()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);
        }

        /*
        Depending on the settings of blue and red lines appearance
        (variables isBlueLine and isBlueLine) redraws them according
        to current coordinates of v1 and v2. Also updates the hints.
        */
        private void RedrawLines()
        {
            LoadScreen();

            if (isBlueLine && !isRedLine)
            {
                DrawLine(v1, pen_darkblue, pen_blue);
            }
            else if (isRedLine && !isBlueLine)
            {
                DrawLine(v2, pen_darkred, pen_red);
            }
            else if (isBlueLine && isRedLine)
            {
                DrawLine(v1, pen_darkblue, pen_blue);
                DrawLine(v2, pen_darkred, pen_red);
            }

            UpdateTooltipAngles();
            pictureBox.Refresh();
            UpdateLabelAngles();
        }

        /*
        To improve visual appearance there are two lines shifted by one pixel
        on the X-axis relative to each other.
        The additional shift by one pixel is required to move the line beginning
        to the origin of coordinates strictly because the form size is 500x500
        (the true center of the form may be determined only if the number of
        points is odd).
        The coordinates of points on the form are counted from the upper left
        corner, so vector coordinates transformation is required.
        */
        private void DrawLine(Vector v, Pen pen1, Pen pen2)
        {
            graphics.DrawLine(
                pen2,
                ConvertCoordsXtoX(v.X1) - 1,
                ConvertCoordsYtoY(v.Y1) - 1,
                ConvertCoordsXtoX(v.X2) - 1,
                ConvertCoordsYtoY(v.Y2) - 1);
            graphics.DrawLine(
                pen1,
                ConvertCoordsXtoX(v.X1) - 2,
                ConvertCoordsYtoY(v.Y1) - 1,
                ConvertCoordsXtoX(v.X2) - 2,
                ConvertCoordsYtoY(v.Y2) - 1);
        }

        /*
        Depending on the settings of blue and red lines appearance
        (variables isBlueLine and isBlueLine) the method updates
        current state of vectors v1 and v2 in labels statusLabelBlue,
        statusLabelRed and statusLabelDelta.
        */
        private void UpdateLabelAngles()
        {
            string str_blue = string.Format("Blue: {0}", GetAngleValueVectorString(v1, 6));
            string str_red = string.Format("Red: {0}", GetAngleValueVectorString(v2, 6));
            string str_delta = string.Format("Delta: {0}", GetAngleValueDeltaString(v1, v2, 6));
            
            if (isBlueLine && !isRedLine)
            {
                statusLabelBlue.Text = str_blue;
            }
            else if (isRedLine && !isBlueLine)
            {
                statusLabelRed.Text = str_red;
            }
            else if (isBlueLine && isRedLine)
            {
                statusLabelBlue.Text = str_blue;
                statusLabelRed.Text = str_red;
                statusLabelDelta.Text = str_delta;
            }
        }

        // A common method to increase form opacity and update hints.
        private void OpacityUp()
        {
            Opacity += 0.1;
            UpdateTooltipOpacity();
        }

        // A common method to decrease form opacity and update hints.
        // The minimum opacity is limited.
        private void OpacityDown()
        {
            if (Opacity > 0.4)
            {
                Opacity += -0.1;
                UpdateTooltipOpacity();
            }
        }

        // The mouse wheel scrolling event handler changes the opacity
        // of the form.
        private void this_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                { OpacityUp(); }
            else
                { OpacityDown(); }
        }

        // Copies the vector angle value transmitted as a parameter
        // to the clipboard.
        private void CopyToClipboardVector(Vector vector)
        {
            Clipboard.SetText(GetAngleValueVectorString(vector, 12));
        }

        // Copies the value of the minimum difference of vectors' angles
        // transmitted as parameters to the clipboard.
        private void CopyToClipboardDelta(Vector vector1, Vector vector2)
        {
            Clipboard.SetText(GetAngleValueDeltaString(vector1, vector2, 12));
        }

        // Event handler for clicking the form opacity increase button.
        private void buttonOpacityIncrease_Click(object sender, EventArgs e)
        {
            OpacityUp();
        }

        // Event handler for clicking the form opacity decrease button.
        private void buttonOpacityDecrease_Click(object sender, EventArgs e)
        {
            OpacityDown();
        }

        // Event handler for clicking the button of copying the angle
        // of vector v1 to the clipboard.
        private void buttonCopyToClipboardBlue_Click(object sender, EventArgs e)
        {
            CopyToClipboardVector(v1);
        }

        // Event handler for clicking the button of copying the angle
        // of vector v2 to the clipboard.
        private void buttonCopyToClipboardRed_Click(object sender, EventArgs e)
        {
            CopyToClipboardVector(v2);
        }

        // Event handler for clicking the button of copying the minimum
        // difference between angle values of vectors v1 and v2 to the clipboard.          
        private void buttonCopyToClipboardDelta_Click(object sender, EventArgs e)
        {
            CopyToClipboardDelta(v1, v2);
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        // The checkbox state change event handler that changes 
        // the form visibility always on top of all windows property.
        private void checkBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAlwaysOnTop.Checked)
                { TopMost = true; }
            else
                { TopMost = false; }
        }

        private void ShowLineBlue()
        {
            if (checkBoxShowLineBlue.Checked)
            {
                isBlueLine = true;
                statusLabelBlue.Visible = true;
                if (isRedLine) { statusLabelDelta.Visible = true; }
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

        /*
        Event handler of changing checkbox state. Switches visibility
        of the blue line and redraws all the lines depending on the
        settings of blue and red lines appearance.
        */
        private void checkBoxShowLineBlue_CheckedChanged(object sender, EventArgs e)
        {
            ShowLineBlue();
        }

        private void ShowLineRed()
        {
            if (checkBoxShowLineRed.Checked)
            {
                isRedLine = true;
                statusLabelRed.Visible = true;
                if (isBlueLine) { statusLabelDelta.Visible = true; }
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



        /*
        Event handler of changing checkbox state. Switches visibility
        of the red line and redraws all the lines depending on the
        settings of blue and red lines appearance.
        */
        private void checkBoxShowLineRed_CheckedChanged(object sender, EventArgs e)
        {
            ShowLineRed();
        }
        
        private void checkBoxLockDelta_CheckedChanged(object sender, EventArgs e)
        {
            ToggleLockDelta();
        }
        
        private void ToggleLockDelta()
        {
            isDeltaAngleLocked = !isDeltaAngleLocked;
            UpdateLabelAngles();
        }

        /*
        Returns as a string the angle value in angular units:
        degrees, minutes and seconds by converting a decimal 
        part of a specified parameter a.
        */
        private string ConvertAngleToDegrees(double a)
        {
            double part_integer = Math.Truncate(a);
            double part_decimal = a - Math.Truncate(a);
            double m = Math.Truncate(part_decimal * 60);
            double s = Math.Truncate(part_decimal * 3600) - Math.Truncate(m) * 60;

            string str_result = string.Format("{0}\u00b0 {1}' {2}\u0022", part_integer, m, s);// \u0022 - Unicode symbol "
            
            return str_result;    
        }

        /*
        Returns as a string the angle value of the vector transmitted
        as a parameter depending on the representation of the decimal
        part (variable isAngleInDegrees).
        The parameter r sets the number of fractional digits in the 
        return value.
        */
        private string GetAngleValueVectorString(Vector vector, int digits)
        {
            string str_result;

            if (isAngleInDegrees)
                { str_result = string.Format("{0}", ConvertAngleToDegrees(vector.A)); }
            else
                { str_result = string.Format("{0}\u00b0", Convert.ToString(Math.Round(vector.A, digits))); }
            
            return str_result;
        }

        /*
        Returns as a string the minimum difference between angle values
        of vectors vector1 and vector2 transmitted as a parameters depending
        on the representation of the decimal part (variable isAngleInDegrees).
        The parameter r sets the number of fractional digits in the 
        return value.
        */
        private string GetAngleValueDeltaString(Vector vector1, Vector vector2, int digits)
        {
            string str_result;
            
            string str_locked = "";
            if (isDeltaAngleLocked)
                { str_locked = "[L]"; }
            
            if (isAngleInDegrees)
                { str_result = string.Format("{0} {1}", ConvertAngleToDegrees(AverageAngleMin(vector1.A, vector2.A, digits)), str_locked); }
            else
                { str_result = string.Format("{0}\u00b0 {1}", AverageAngleMin(vector1.A, vector2.A, digits), str_locked); }
            
            return str_result;
        }

        /*
        The coordinates of points on the form are counted from the upper
        left corner. The method converts the true integer value of the
        X-coordinate into an equivalent coordinate on the form.
        */
        private int ConvertCoordsXtoX(int x) { return Convert.ToInt32(x + pictureBox.Width / 2); }

        /*
        The coordinates of points on the form are counted from the upper
        left corner. The method converts the true integer value of the
        Y-coordinate into an equivalent coordinate on the form.
        */
        private int ConvertCoordsYtoY(int y) { return Convert.ToInt32(-y + pictureBox.Height / 2); }

        /*
        The coordinates of points on the form are counted from the upper
        left corner. The method converts the true double value of the
        X-coordinate into an equivalent integer coordinate on the form.
        */
        private int ConvertCoordsXtoX(double x)
        {
            try
            {
                // The unsafe type conversion requires exception processing.
                int input = Convert.ToInt32(x);
                int output = ConvertCoordsXtoX(input);
                return output;
            }
            catch (Exception) { return 0; }
        }

        /*
        The coordinates of points on the form are counted from the upper
        left corner. The method converts the true double value of the
        Y-coordinate into an equivalent integer coordinate on the form.
        */
        private int ConvertCoordsYtoY(double y)
        {
            try
            {
                // The unsafe type conversion requires exception processing.
                int input = Convert.ToInt32(y);
                int output = ConvertCoordsYtoY(input);
                return output;
            }
            catch (Exception) { return 0; }
        }

        // Event handler to stop changing new vector coordinates.
        // Uses an object e as a mouse event handler.
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { isDownLMB = false; }
            if (e.Button == MouseButtons.Right) { isDownRMB = false; }
        }

        // Event handler to start changing new vector coordinates.
        // Uses an object e as a mouse event handler.
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                { isDownLMB = true; }
            if (e.Button == MouseButtons.Right)
                { isDownRMB = true; }

            if (isBlueLine && e.Button == MouseButtons.Left)
            {
                RefreshVector(v1, sender, e);
            }
            if (isRedLine && e.Button == MouseButtons.Right)
            {
                RefreshVector(v2, sender, e);
            }
        }

        /*
        Event handler to change vector coordinates in real time mode
        Uses the state of variables isDownLMB and isDownRMB from the
        pictureBox_MouseDown() event handler.
        */
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDownLMB && e.Button == MouseButtons.Left && isBlueLine)
            {
                RefreshVector(v1, sender, e);
            }
            if (isDownRMB && e.Button == MouseButtons.Right && isRedLine)
            {
                RefreshVector(v2, sender, e);
            }
        }

        // Combined method to updates vector's state and redraw the lines.
        private void RefreshVector(Vector vector, object sender, MouseEventArgs e)
        {
            UpdateVectors(vector, sender, e);
            RedrawLines();
        }

        // Gets a vector as a parameter and updates its state depending on
        // the cursor coordinates. Uses an object e as a mouse event handler.
        private void UpdateVectors(Vector vector_base, object sender, MouseEventArgs e)
        {
            int new_x = ((e.X) - pictureBox.Width / 2);
            int new_y = -((e.Y) - pictureBox.Height / 2);
            double new_angle = Vector.GetAngleByPoints(0, 0, new_x, new_y);

            if (isDeltaAngleLocked == true)
            {
                double old_angle = vector_base.A;
                double diff_angle = new_angle - old_angle;

                if (vector_base == v1)
                    { v2.SetVectorByAngle(0, 0, LENGTH_VISUAL, RecalculateAngle(v2.A + diff_angle)); }

                if (vector_base == v2)
                    { v1.SetVectorByAngle(0, 0, LENGTH_VISUAL, RecalculateAngle(v1.A + diff_angle)); }
            }

            vector_base.SetVectorByAngle(0, 0, LENGTH_VISUAL, RecalculateAngle(new_angle));
        }

        /*
         * Some key presses, such as the TAB, RETURN, ESC, and arrow keys, are typically
         * ignored by some controls because they are not considered input key presses.
         * For example, by default, a Button control ignores the arrow keys.
         * Pressing the arrow keys typically causes the focus to move to the previous
         * or next control. The arrow keys are considered navigation keys and pressing
         * these keys typically do not raise the KeyDown event for a Button.
         * However, pressing the arrow keys for a Button does raise the PreviewKeyDown event.
         * By handling the PreviewKeyDown event for a Button and setting the IsInputKey
         * property to true, you can raise the KeyDown event when the arrow keys are pressed.
         * However, if you handle the arrow keys, the focus will no longer move
         * to the previous or next control.
         *
         * See MSDN Control.PreviewKeyDown Event 
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

        private void ShowHelp()
        {
            string msg_text = "Protractor 1.2 64-bit\n" +
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
                              "Move the window with the arrow keys [\u2191], [\u2193], [\u2190], [\u2192] by 1 px." +
                              " Hold [Ctrl] or [Alt] to move the window by 10 px or 50 px, respectively." +
                              " Press [Home] to center the window on the screen.\n\n";
            
            string msg_caption = "Information";
            var msg_buttons = MessageBoxButtons.OK;
            var msg_icon = MessageBoxIcon.Information;

            MessageBox.Show(this, msg_text, msg_caption, msg_buttons, msg_icon);
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
                else if (keyData == Keys.Down)
                { FormMoveDirection("DOWN", value); }
                else if (keyData == Keys.Left)
                { FormMoveDirection("LEFT", value); }
                else if (keyData == Keys.Right)
                { FormMoveDirection("RIGHT", value); }

                return true;
            }
    
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Hotkeys processing. Uses an object e as a keyboard event handler.
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
                SetAngleArbitrary(sender, e, "BLUE");
                e.Handled = true;
            }
            
            if (e.KeyCode == Keys.D2 && e.Control)
            {
                SetAngleArbitrary(sender, e, "RED");
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

        /*
        Returns Delta angle - the minimum value of difference between
        a1 and a2 corners. The parameter r sets the number of fractional
        digits in the return value.
        */
        private double AverageAngleMin(double a1, double a2, int r)
        {
            double delta = Math.Round(Math.Abs(a1 - a2), r);
            
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
        
        private double GetAngleValueDelta(Vector v1, Vector v2)
        {
            double delta = Math.Abs(v1.A - v2.A);

            if (v1.A >= v2.A)
                { return delta; }
            else
                { return -delta;}
        }

        /*
        The checkbox menuToolAngular state change event handler that calls
        changing the representation of the decimal part of the angle value  
        to angular units.      
        */
        private void menuToolAngular_CheckedChanged(object sender, EventArgs e)
        {
            if (menuToolAngular.Checked)
                { SetAngular(); }
            else 
                { SetDecimal(); }
        }

        /*
        The checkbox menuToolDecimal state change event handler that calls
        changing the representation of the decimal part of the angle value  
        to decimal numbers.      
        */
        private void menuToolDecimal_CheckedChanged(object sender, EventArgs e)
        {
            if (menuToolDecimal.Checked)
                { SetDecimal(); }
            else 
                { SetAngular(); }
        }

        // Changes the representation of the decimal part of the angle value
        // (associated with isAngleInDegrees variable) to angular units.
        private void SetAngular()
        {
            if (isAngleInDegrees == false)
            {
                isAngleInDegrees = true;
                menuToolAngular.Checked = true;
                menuToolDecimal.Checked = false;
                UpdateLabelAngles();
                UpdateTooltipAngles();
            }
        }

        // Changes the representation of the decimal part of the angle value
        // (associated with isAngleInDegrees variable) to decimal numbers.
        private void SetDecimal()
        {
            if (isAngleInDegrees == true)
            {
                isAngleInDegrees = false;
                menuToolAngular.Checked = false;
                menuToolDecimal.Checked = true;
                UpdateLabelAngles();
                UpdateTooltipAngles();
            }
        }

        // Sets a new vector position by angle (used in the statusToolStrip menu).
        // After that redraws the lines.
        private void SetVectorAngle(Vector vector, double angle)
        {
            double delta = GetAngleValueDelta(v1, v2);
            
            vector.SetVectorByAngle(0, 0, LENGTH_VISUAL, angle);
            
            if (isDeltaAngleLocked == true)
            {
                if (vector == v1)
                    { v2.SetVectorByAngle(0, 0, LENGTH_VISUAL, RecalculateAngle(angle - delta)); }
                
                if (vector == v2)
                    { v1.SetVectorByAngle(0, 0, LENGTH_VISUAL, RecalculateAngle(angle + delta)); }
            }
            
            RedrawLines();
        }
        
        private void SetAngleArbitrary(object sender, EventArgs e, string color)
        {
            string title = "Arbitrary angle value";
            string prompt = string.Format("Enter the {0} angle value:", color);
            double default_value = 0.0;
            
            using (var dialog = new InputDialog(title, prompt, default_value))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (color == "BLUE")
                    {
                        SetVectorAngle(v1, dialog.Value);
                    }
                    if (color == "RED")
                    {
                        SetVectorAngle(v2, dialog.Value);
                    }
                }
            }
        }

        // The group of event handlers to select one of the fixed
        // angle values of vector v1 in the statusToolStrip component.
        private void toolSetAngleBlue0_Click(object sender, EventArgs e) { SetVectorAngle(v1, 0); }
        private void toolSetAngleBlue30_Click(object sender, EventArgs e) { SetVectorAngle(v1, 30); }
        private void toolSetAngleBlue45_Click(object sender, EventArgs e) { SetVectorAngle(v1, 45); }
        private void toolSetAngleBlue60_Click(object sender, EventArgs e) { SetVectorAngle(v1, 60); }
        private void toolSetAngleBlue90_Click(object sender, EventArgs e) { SetVectorAngle(v1, 90); }
        private void toolSetAngleBlue120_Click(object sender, EventArgs e) { SetVectorAngle(v1, 120); }
        private void toolSetAngleBlue180_Click(object sender, EventArgs e) { SetVectorAngle(v1, 180); }
        private void toolSetAngleBlue270_Click(object sender, EventArgs e) { SetVectorAngle(v1, 270); }

        private void toolSetAngleBlueArbitrary_Click(object sender, EventArgs e)
        {
            SetAngleArbitrary(sender, e, "BLUE");
        }

        // The group of event handlers to select one of the fixed
        // angle values of vector v2 in the statusToolStrip component.
        private void toolSetAngleRed0_Click(object sender, EventArgs e) { SetVectorAngle(v2, 0); }
        private void toolSetAngleRed30_Click(object sender, EventArgs e) { SetVectorAngle(v2, 30); }
        private void toolSetAngleRed45_Click(object sender, EventArgs e) { SetVectorAngle(v2, 45); }
        private void toolSetAngleRed60_Click(object sender, EventArgs e) { SetVectorAngle(v2, 60); }
        private void toolSetAngleRed90_Click(object sender, EventArgs e) { SetVectorAngle(v2, 90); }
        private void toolSetAngleRed120_Click(object sender, EventArgs e) { SetVectorAngle(v2, 120); }
        private void toolSetAngleRed180_Click(object sender, EventArgs e) { SetVectorAngle(v2, 180); }
        private void toolSetAngleRed270_Click(object sender, EventArgs e) { SetVectorAngle(v2, 270); }

        private void toolSetAngleRedArbitrary_Click(object sender, EventArgs e)
        {
            SetAngleArbitrary(sender, e, "RED");
        }

        /*
        To avoid incorrect scaling of components on the form 
        by the operating system (100%, 125%, 150%) their sizes
        and positions are set hard in the form constructor.     
        */
        private void SetFormProperties()
        {
            statusStrip1.Size = new System.Drawing.Size(240, 24);
            statusStrip1.BackColor = Color.FromArgb(255, 212, 208, 200);

            ClientSize = new System.Drawing.Size(500, 500 + statusStrip1.Size.Height + 4);
            BackColor = Color.FromArgb(255, 212, 208, 200);

            pictureBox.Location = new System.Drawing.Point(0, 0);
            pictureBox.Size = new System.Drawing.Size(500, 500);

            // top left block
            buttonOpacityIncrease.Size = new System.Drawing.Size(30, 30);
            buttonOpacityIncrease.Location = new System.Drawing.Point(72, 4);

            buttonOpacityDecrease.Size = new System.Drawing.Size(30, 30);
            buttonOpacityDecrease.Location = new System.Drawing.Point(38, 4);

            checkBoxAlwaysOnTop.Size = new System.Drawing.Size(30, 30);
            checkBoxAlwaysOnTop.Location = new System.Drawing.Point(4, 4);
            
            buttonHelp.Size = new System.Drawing.Size(30, 30);
            //buttonHelp.Location = new System.Drawing.Point(4, 38);
            buttonHelp.Location = new System.Drawing.Point(466, 4);

            // bottom left block
            buttonCopyToClipboardBlue.Size = new System.Drawing.Size(30, 30);
            buttonCopyToClipboardBlue.Location = new System.Drawing.Point(4, 468);

            buttonCopyToClipboardRed.Size = new System.Drawing.Size(30, 30);
            buttonCopyToClipboardRed.Location = new System.Drawing.Point(38, 468);

            buttonCopyToClipboardDelta.Size = new System.Drawing.Size(30, 30);
            buttonCopyToClipboardDelta.Location = new System.Drawing.Point(72, 468);

            checkBoxLockDelta.Location = new System.Drawing.Point(4, 400);
            checkBoxLockDelta.Size = new System.Drawing.Size(30, 30);
            
            checkBoxShowLineBlue.Location = new System.Drawing.Point(4, 434);
            checkBoxShowLineBlue.Size = new System.Drawing.Size(30, 30);

            checkBoxShowLineRed.Location = new System.Drawing.Point(38, 434);
            checkBoxShowLineRed.Size = new System.Drawing.Size(30, 30);
            

            // bottom block
            statusLabelBlue.Font = new System.Drawing.Font("Tahoma", 10F);
            statusLabelBlue.ForeColor = System.Drawing.Color.DarkBlue;
            statusLabelBlue.Text = "";

            statusLabelRed.Font = new System.Drawing.Font("Tahoma", 10F);
            statusLabelRed.ForeColor = System.Drawing.Color.DarkRed;
            statusLabelRed.Text = "";

            statusLabelDelta.Font = new System.Drawing.Font("Tahoma", 10F);
            statusLabelDelta.ForeColor = System.Drawing.Color.Black;
            statusLabelDelta.Text = "";

            menuToolAngular.Text = "Format: d\u00b0 m' s\u0022 (A)"; // \u0022 unicode symbol "
            menuToolDecimal.Text = "Format: d,nnn\u00b0 (D)"; // \u00b0 unicode symbol °
        }
    }
}