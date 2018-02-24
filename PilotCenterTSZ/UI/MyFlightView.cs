using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PilotCenterTSZ.UI
{
    public partial class MyFlightView : UserControl
    {
        public MyFlightView()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();

            var a = this.Parent as MyLogBookCtrl;

            a.lstLogBook.Show();


        }
    }
}
