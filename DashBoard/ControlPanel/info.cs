using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPanel
{
    public partial class info : Form
    {
        public info()
        {
            InitializeComponent();
        }

        private void info_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'frameDataSet.info' table. You can move, or remove it, as needed.
            this.infoTableAdapter.Fill(this.frameDataSet.info);

        }
    }
}
