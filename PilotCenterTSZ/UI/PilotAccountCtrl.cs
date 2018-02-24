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

        public PilotAccountCtrl()
        {
            InitializeComponent();
            PilotInfos();
            Typeratings();
            Qualifications();
            Efficiency();
            AwardHour();
        }

        public void Actions()
        {
            PilotInfos();
            Typeratings();
            Qualifications();
            Efficiency();
            AwardHour();
        }

        public void PilotInfos()
        {
            UserInfo a = new UserInfo();

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
            listTyperatings.Items.Clear();
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

        public void Qualifications()
        {
            listQualifications.Items.Clear();
            foreach (Qualification t in Qualification.Get())
            {
                listQualifications.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listQualifications.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listQualifications.Items.Add(new ListViewItem(new string[]
                {
                    t.QualificationName,
                    t.Validity.ToString(@"dd-MM-yyyy"),
                    t.Expiration.ToString(@"dd-MM-yyyy")

                }));

            }
        }

        public void Efficiency()
        {
            UserOverallEfficiency h = new UserOverallEfficiency();

            cProgressBarOverall.Value = h.Efficiency;

            if (h.Efficiency < 25)
            {
                cProgressBarOverall.ForeColor = Color.Firebrick;
                cProgressBarOverall.ProgressColor = Color.Firebrick;
            }
            if (h.Efficiency < 50 && h.Efficiency >= 25)
            {
                cProgressBarOverall.ForeColor = Color.Chocolate;
                cProgressBarOverall.ProgressColor = Color.Chocolate;
            }
            if (h.Efficiency < 75 && h.Efficiency >= 50)
            {
                cProgressBarOverall.ForeColor = Color.Goldenrod;
                cProgressBarOverall.ProgressColor = Color.Goldenrod;
            }
            if (h.Efficiency < 100 && h.Efficiency >= 75)
            {
                cProgressBarOverall.ForeColor = Color.ForestGreen;
                cProgressBarOverall.ProgressColor = Color.ForestGreen;
            }
        }

        public void AwardHour()
        {
            UserInfo a = new UserInfo();
            UserHourAward h = new UserHourAward();

            double flightHours = Convert.ToDouble(a.PilotHours.TotalHours.ToString());
            int progressValue = (Convert.ToInt32(Math.Truncate(flightHours)) * 100) / h.AwardMinHours;

            lblHourAward.Text = String.Format("{0} Hour Award", h.AwardEps);

            if (progressValue > 100)
                cProgressHourAward.Value = 100;
            else
                cProgressHourAward.Value = progressValue;


        }
    }
}
