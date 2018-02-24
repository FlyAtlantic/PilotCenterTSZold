using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PilotCenterTSZ
{
    public partial class Dashboard : Form
    {
        UserInfo a = new UserInfo();

        public Dashboard()
        {
            InitializeComponent();

            lblWelcome.Text = String.Format("Have a nice {0}, {1} {2} {3}", DateTime.UtcNow.DayOfWeek.ToString(), a.Rank, a.UserName, a.UserSurname);
        }
      
    }
}
