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
    public partial class opform : Form
    {
        public opform()
        {
            InitializeComponent();
        }

        public opform(String data)
        {
            InitializeComponent();

            
        }

        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            fd.Font = opcodetext.Font;

            if(fd.ShowDialog() == DialogResult.OK)
            {
                opcodetext.Font = fd.Font;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
