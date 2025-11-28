using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Protractor
{
    public partial class FormInputDialog : Form
    {
        public double Value { get; private set; }
    
        public FormInputDialog(string str_title, string str_prompt, double default_value = 0)
        {
            InitializeComponent();
            this.Text = str_title;
            labelPrompt.Text = str_prompt;
            textBoxValue.Text = default_value.ToString();
            

            this.labelPrompt.Location = new Point(12, 15);
            this.labelPrompt.Size = new Size(200, 13);
        
            this.textBoxValue.Location = new Point(12, 35);
            this.textBoxValue.Size = new Size(260, 30);

            this.buttonOK.Location = new Point(116, 70);
            this.buttonOK.Size = new Size(75, 30);
        
            this.buttonCancel.Location = new Point(197, 70);
            this.buttonCancel.Size = new Size(75, 30);
        }
    
        private void buttonOK_Click(object sender, EventArgs e)
        {
            double result;
            string str_input = textBoxValue.Text.Replace(',', '.').Trim();

            if (double.TryParse(str_input, NumberStyles.Any, CultureInfo.InvariantCulture, out result) 
                && (result >= 0) && (result <= 360))
            {
                Value = result;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                string str_text = "Please enter a valid value between 0.0 and 360.0 degrees.";
                string str_caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                
                MessageBox.Show(str_text, str_caption, buttons, icon);
                textBoxValue.Focus();
                textBoxValue.SelectAll();
            }
        }
    
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    
        private void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && 
                e.KeyChar != '.' && e.KeyChar != ',')
                { e.Handled = true; }
        
            if (e.KeyChar == ',')
                { e.KeyChar = '.'; }
        }
    }
}