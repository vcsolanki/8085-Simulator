using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8085_Simulator
{
    public partial class address_editbox : Form
    {

        public int int_value = 0;


        public address_editbox(string hex)
        {
            
            InitializeComponent();
            int_value = Convert.ToInt32(hex, 16);
            value.Text = hex;
        }

        private void dec_flag_CheckedChanged(object sender, EventArgs e)
        {
            if (value.MaxLength == 2)
            {
                int_value = Convert.ToInt32(value.Text, 16);
            }
            else if (value.MaxLength == 8)
            {
                int_value = Convert.ToInt32(value.Text, 2);
            }
            value.MaxLength = 3;
            value.Text = int_value.ToString().PadLeft(3, '0');
        }

        private void hex_flag_CheckedChanged(object sender, EventArgs e)
        {
            if (value.MaxLength == 3)
            {
                int_value = Convert.ToInt32(value.Text, 10);
            }
            else if (value.MaxLength == 8)
            {
                int_value = Convert.ToInt32(value.Text, 2);
            }
            value.MaxLength = 2;
            value.Text = int_value.ToString("X").PadLeft(2, '0');
        }

        private void bin_flag_CheckedChanged(object sender, EventArgs e)
        {
            if (value.MaxLength == 2)
            {
                int_value = Convert.ToInt32(value.Text, 16);
            }
            else if (value.MaxLength == 3)
            {
                int_value = Convert.ToInt32(value.Text, 10);
            }
            value.MaxLength = 8;
            value.Text = Convert.ToString(int_value, 2).PadLeft(8, '0');
        }

        private void value_keypress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.Return)
            {
                DialogResult = DialogResult.OK;
                e.Handled = true;
                Close();
            }
            else if(bin_flag.Checked)
            {
                if(e.KeyChar == (char)Keys.D1 ||
                    e.KeyChar == (char)Keys.D0 ||
                    e.KeyChar == (char)Keys.Back)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if(hex_flag.Checked)
            {
                if ((e.KeyChar <= (char)Keys.D9 &&
                    e.KeyChar >= (char)Keys.D0) ||
                    e.KeyChar == 'A' ||
                    e.KeyChar == 'a' ||
                    e.KeyChar == 'B' ||
                    e.KeyChar == 'b' ||
                    e.KeyChar == 'C' ||
                    e.KeyChar == 'c' ||
                    e.KeyChar == 'D' ||
                    e.KeyChar == 'd' ||
                    e.KeyChar == 'E' ||
                    e.KeyChar == 'e' ||
                    e.KeyChar == 'F' ||
                    e.KeyChar == 'f' ||
                    e.KeyChar == (char)Keys.Back)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if(dec_flag.Checked)
            {
                if (e.KeyChar <= (char)Keys.D9 ||
                   e.KeyChar >= (char)Keys.D0 ||
                    e.KeyChar == (char)Keys.Back)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void value_changed(object sender, EventArgs e)
        {
            if (value.Text != "")
            {
                if (bin_flag.Checked)
                {
                    if(value.MaxLength == 2)
                        int_value = Convert.ToInt32(value.Text.ToUpper(), 16);
                    if (value.MaxLength == 3)
                        int_value = Convert.ToInt32(value.Text.ToUpper(), 10);
                    if (value.MaxLength == 8)
                        int_value = Convert.ToInt32(value.Text.ToUpper(), 2);
                }
                else if (hex_flag.Checked)
                {
                    if (value.MaxLength == 2)
                        int_value = Convert.ToInt32(value.Text.ToUpper(), 16);
                    if (value.MaxLength == 3)
                        int_value = Convert.ToInt32(value.Text.ToUpper(), 10);
                    if (value.MaxLength == 8)
                        int_value = Convert.ToInt32(value.Text.ToUpper(), 2);
                }
                else if (dec_flag.Checked)
                {
                    if (value.MaxLength == 2)
                        int_value = Convert.ToInt32(value.Text.ToUpper(), 16);
                    if (value.MaxLength == 3)
                        int_value = Convert.ToInt32(value.Text.ToUpper(), 10);
                    if (value.MaxLength == 8)
                        int_value = Convert.ToInt32(value.Text.ToUpper(), 2);
                }
            }
        }

        private void set_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
