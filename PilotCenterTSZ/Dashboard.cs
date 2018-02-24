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

        public void AllHides()
        {
            flightAssignmentCtrl.Hide();
            pilotAccountCtrl.Hide();
        }

        public Dashboard()
        {
            InitializeComponent();

            lblWelcome.Text = String.Format("Have a nice {0}, {1} {2} {3}", DateTime.UtcNow.DayOfWeek.ToString(), a.Rank, a.UserName, a.UserSurname);

            VerifyAndDeleteFlight.Start();

            AssignFlight.VerifyFlightTimeOut();

            flightAssignmentCtrl.Actions();

            flightAssignmentCtrl.flightAssignedCtrl.UpdateInfos();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            pilotAccountCtrl.Actions();
            flightAssignmentCtrl.Actions();
            flightAssignmentCtrl.flightAssignedCtrl.UpdateInfos();
        }

        private void btnAssignFlight_Click(object sender, EventArgs e)
        {
            AllHides();
            flightAssignmentCtrl.Show();
        }

        private void VerifyAndDeleteFlight_Tick(object sender, EventArgs e)
        {
            AssignFlight.VerifyFlightTimeOut();

            flightAssignmentCtrl.Actions();

            flightAssignmentCtrl.flightAssignedCtrl.UpdateInfos();
        }

        private void btnPilotCenter_Click(object sender, EventArgs e)
        {
            AllHides();
            pilotAccountCtrl.Show();
        }
    }
}
