/*
The application allows to measure the angles between
objects relative to the center of the angular grid.
The Graphics.Drawline() method of the System.Drawing
namespace is used for additional graphical drawings.
The library of vector methods is located in Vector 
class in VectorLibrary.cs file.
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

        // The object v1 represents the blue line.
        private Vector v1 = new Vector(0, 0, 205, 0, 1);

        // The object v2 represents the red line.
        private Vector v2 = new Vector(0, 0, 205, 0, 1);

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

            UpdateTooltipOpacity();
            UpdateTooltipAngles();
        }

        /*
        A common method to change the hints about the current opacity
        of the form for buttonOpacityIncrease and buttonOpacityDecrease
        buttons using toolTipMain component.
        */
        private void UpdateTooltipOpacity()
        {
            toolTipMain.SetToolTip(buttonOpacityIncrease,
            $"Current opacity: {Convert.ToString(Opacity * 100)}%\nIncrease opacity (Mouse Wheel Up)");
            toolTipMain.SetToolTip(buttonOpacityDecrease,
            $"Current opacity: {Convert.ToString(Opacity * 100)}%\nDecrease opacity (Mouse Wheel Down)");
        }

        /*
        The common method to change the hints about the current values
        of angles of vectors v1 and v2 for buttonCopyToClipboardBlue,
        buttonCopyToClipboardRed and buttonCopyToClipboardDelta using
        toolTipMain component.
        */
        private void UpdateTooltipAngles()
        {
            toolTipMain.SetToolTip(buttonCopyToClipboardBlue,
            $"Blue angle value: {GetAngleValueVector(v1, 12)}\nCopy to the clipboard (Ctrl + X)");
            toolTipMain.SetToolTip(buttonCopyToClipboardRed,
            $"Red angle value: {GetAngleValueVector(v2, 12)}\nCopy to the clipboard (Ctrl + C)");
            toolTipMain.SetToolTip(buttonCopyToClipboardDelta,
            $"Delta angle value: {GetAngleValueDelta(v1, v2, 12)}\nCopy to the clipboard (Ctrl + V)");
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
                DrawLine(v1, Pens.DarkBlue, Pens.Blue);
            }
            else if (isRedLine && !isBlueLine)
            {
                DrawLine(v2, Pens.DarkRed, Pens.Red);
            }
            else if (isBlueLine && isRedLine)
            {
                DrawLine(v1, Pens.DarkBlue, Pens.Blue);
                DrawLine(v2, Pens.DarkRed, Pens.Red);
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
            if (isBlueLine && !isRedLine)
            {
                statusLabelBlue.Text = $"Blue: {GetAngleValueVector(v1, 6)}";
            }
            else if (isRedLine && !isBlueLine)
            {
                statusLabelRed.Text = $"Red: {GetAngleValueVector(v2, 6)}";
            }
            else if (isBlueLine && isRedLine)
            {
                statusLabelBlue.Text = $"Blue: {GetAngleValueVector(v1, 6)}";
                statusLabelRed.Text = $"Red: {GetAngleValueVector(v2, 6)}";
                statusLabelDelta.Text = $"Delta: {GetAngleValueDelta(v1, v2, 6)}";
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
            if (e.Delta > 0) { OpacityUp(); }
            else { OpacityDown(); }
        }

        // Copies the vector angle value transmitted as a parameter
        // to the clipboard.
        private void CopyToClipboardVector(Vector vector)
        {
            Clipboard.SetText(GetAngleValueVector(vector, 12));
        }

        // Copies the value of the minimum difference of vectors' angles
        // transmitted as parameters to the clipboard.
        private void CopyToClipboardDelta(Vector vector1, Vector vector2)
        {
            Clipboard.SetText(GetAngleValueDelta(vector1, vector2, 12));
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

        // The checkbox state change event handler that changes 
        // the form visibility always on top of all windows property.
        private void checkBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAlwaysOnTop.Checked) { TopMost = true; }
            else { TopMost = false; }
        }

        /*
        Event handler of changing checkbox state. Switches visibility
        of the blue line and redraws all the lines depending on the
        settings of blue and red lines appearance.
        */
        private void checkBoxShowLineBlue_CheckedChanged(object sender, EventArgs e)
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
        of the red line and redraws all the lines depending on the
        settings of blue and red lines appearance.
        */
        private void checkBoxShowLineRed_CheckedChanged(object sender, EventArgs e)
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
        Returns as a string the angle value in angular units:
        degrees, minutes and seconds by converting a decimal 
        part of a specified parameter a.
        */
        private string ConvertAngleToDegrees(double a)
        {
            double integerpart = Math.Truncate(a);
            double decimalpart = a - Math.Truncate(a);
            double m = Math.Truncate(decimalpart * 60);
            double s = Math.Truncate(decimalpart * 3600) - Math.Truncate(m) * 60;
            return $"{integerpart}\u00b0 {m}' {s}\u0022";    // \u0022 - Unicode symbol "
        }

        /*
        Returns as a string the angle value of the vector transmitted
        as a parameter depending on the representation of the decimal
        part (variable isAngleInDegrees).
        The parameter r sets the number of fractional digits in the 
        return value.
        */
        private string GetAngleValueVector(Vector vector, byte r)
        {
            if (isAngleInDegrees) { return $"{ConvertAngleToDegrees(vector.A)}"; }
            else { return $"{Convert.ToString(Math.Round(vector.A, r))}\u00b0"; }
        }

        /*
        Returns as a string the minimum difference between angle values
        of vectors vector1 and vector2 transmitted as a parameters depending
        on the representation of the decimal part (variable isAngleInDegrees).
        The parameter r sets the number of fractional digits in the 
        return value.
        */
        private string GetAngleValueDelta(Vector vector1, Vector vector2, byte r)
        {
            if (isAngleInDegrees) { return $"{ConvertAngleToDegrees(AverageAngleMin(vector1.A, vector2.A, r))}"; }
            else { return $"{AverageAngleMin(vector1.A, vector2.A, r)}\u00b0"; }
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
            if (e.Button == MouseButtons.Left) { isDownLMB = true; }
            if (e.Button == MouseButtons.Right) { isDownRMB = true; }

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
            UpdateVector(vector, sender, e);
            RedrawLines();
        }

        // Gets a vector as a parameter and updates its state depending on
        // the cursor coordinates. Uses an object e as a mouse event handler.
        private void UpdateVector(Vector vector, object sender, MouseEventArgs e)
        {
            int newX = ((e.X) - pictureBox.Width / 2);   // New X-coordinate
            int newY = -((e.Y) - pictureBox.Height / 2); // New Y-coordinate
            double newA = Vector.GetAngleByPoints(0, 0, newX, newY); // New angle
            double m = 205; // Standard length of support vector for current grid

            vector.SetVectorByAngle(0, 0, m, newA);
        }

        // Hotkeys processing. Uses an object e as a keyboard event handler.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X && e.Control)
            {
                CopyToClipboardVector(v1);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.C && e.Control)
            {
                CopyToClipboardVector(v2);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.V && e.Control)
            {
                CopyToClipboardDelta(v1, v2);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Q && e.Control)
            {
                SetAngular();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.W && e.Control)
            {
                SetDecimal();
                e.Handled = true;
            }
        }

        /*
        Returns Delta angle - the minimum value of difference between
        a1 and a2 corners. The parameter r sets the number of fractional
        digits in the return value.
        */
        private double AverageAngleMin(double a1, double a2, byte r)
        {
            double delta = Math.Round(Math.Abs(a1 - a2), r);
            if (delta > 180) { return 360 - delta; }
            else { return delta; }
        }

        /*
        The checkbox menuToolAngular state change event handler that calls
        changing the representation of the decimal part of the angle value  
        to angular units.      
        */
        private void menuToolAngular_CheckedChanged(object sender, EventArgs e)
        {
            if (menuToolAngular.Checked) { SetAngular(); }
            else { SetDecimal(); }
        }

        /*
        The checkbox menuToolDecimal state change event handler that calls
        changing the representation of the decimal part of the angle value  
        to decimal numbers.      
        */
        private void menuToolDecimal_CheckedChanged(object sender, EventArgs e)
        {
            if (menuToolDecimal.Checked) { SetDecimal(); }
            else { SetAngular(); }
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
            vector.SetVectorByAngle(0, 0, 205, angle);
            RedrawLines();
        }

        // The group of event handlers to select one of the fixed
        // angle values of vector v1 in the statusToolStrip component.
        private void toolSetAngleBlue0_Click(object sender, EventArgs e) { SetVectorAngle(v1, 0); }
        private void toolSetAngleBlue90_Click(object sender, EventArgs e) { SetVectorAngle(v1, 90); }
        private void toolSetAngleBlue180_Click(object sender, EventArgs e) { SetVectorAngle(v1, 180); }
        private void toolSetAngleBlue270_Click(object sender, EventArgs e) { SetVectorAngle(v1, 270); }

        // The group of event handlers to select one of the fixed
        // angle values of vector v2 in the statusToolStrip component.
        private void toolSetAngleRed0_Click(object sender, EventArgs e) { SetVectorAngle(v2, 0); }
        private void toolSetAngleRed90_Click(object sender, EventArgs e) { SetVectorAngle(v2, 90); }
        private void toolSetAngleRed180_Click(object sender, EventArgs e) { SetVectorAngle(v2, 180); }
        private void toolSetAngleRed270_Click(object sender, EventArgs e) { SetVectorAngle(v2, 270); }

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
            buttonOpacityIncrease.Location = new System.Drawing.Point(4, 4);

            buttonOpacityDecrease.Size = new System.Drawing.Size(30, 30);
            buttonOpacityDecrease.Location = new System.Drawing.Point(38, 4);

            checkBoxAlwaysOnTop.Location = new System.Drawing.Point(72, 4);
            checkBoxAlwaysOnTop.Size = new System.Drawing.Size(30, 30);

            // bottom left block
            buttonCopyToClipboardBlue.Size = new System.Drawing.Size(30, 30);
            buttonCopyToClipboardBlue.Location = new System.Drawing.Point(4, 468);

            buttonCopyToClipboardRed.Size = new System.Drawing.Size(30, 30);
            buttonCopyToClipboardRed.Location = new System.Drawing.Point(38, 468);

            buttonCopyToClipboardDelta.Size = new System.Drawing.Size(30, 30);
            buttonCopyToClipboardDelta.Location = new System.Drawing.Point(72, 468);

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

            menuToolAngular.Text = "Format: d\u00b0 m' s\u0022 (Ctrl + Q)"; // \u0022 unicode symbol "
            menuToolDecimal.Text = "Format: d,nnn\u00b0 (Ctrl + W)"; // \u00b0 unicode symbol °
        }
    }
}