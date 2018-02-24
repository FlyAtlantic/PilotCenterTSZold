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
    public partial class PilotAccountCtrl : UserControl
    {
        UserInfo a = new UserInfo();

        public PilotAccountCtrl()
        {
            InitializeComponent();
            PilotInfos();
            Typeratings();
        }

        public void PilotInfos()
        {
            double flightHours = Convert.ToDouble(a.PilotHours.TotalHours.ToString());
            double flightMinutes = Convert.ToDouble(a.PilotHours.Minutes);

            lblName.Text = a.UserName + ' ' +a.UserSurname;
            lblRank.Text = a.Rank;
            lblRatingVacc.Text = a.Rate;
            lblCallsign.Text = "TSZ" + a.Callsign;
            lblFlightHours.Text = Math.Truncate(flightHours).ToString()+ ':' + Math.Truncate(flightMinutes).ToString();
            lblLastFlightDate.Text = a.LastFlight.ToString(@"dd-MM-yyyy");
            lblHub.Text = a.Hub;
            lblLocation.Text = a.Location;
            lblEps.Text = a.Eps.ToString();

            switch (a.RankID)
            {
                case 1:
                    imgRank.Image = Properties.Resources.student;
                    break;
                case 2:
                    imgRank.Image = Properties.Resources.seniorstudent;
                    break;
                case 3:
                    imgRank.Image = Properties.Resources.secondofficer;
                    break;
                case 4:
                    imgRank.Image = Properties.Resources.firstofficer;
                    break;
                case 5:
                    imgRank.Image = Properties.Resources.captain;
                    break;
                case 6:
                    imgRank.Image = Properties.Resources.seniorcaptain;
                    break;
                case 7:
                    imgRank.Image = Properties.Resources.flightinstructor;
                    break;

                default:
                    break;
            }
        }

        public void Typeratings()
        {

            foreach (TypeRating t in TypeRating.Get())
            {
                listTyperatings.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listTyperatings.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listTyperatings.Items.Add(new ListViewItem(new string[]
                {
                    t.TypeRatingName,
                    t.Validity.ToString(@"dd-MM-yyyy"),
                    t.Expiration.ToString(@"dd-MM-yyyy")

                }));

            }
        }
    }
}
