﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ExamCenterTSZ.Functions;

namespace PilotCenterTSZ.UI
{
    public partial class AssingmentCtrl : UserControl
    {


        public AssingmentCtrl()
        {
            InitializeComponent();
            comboTypeRatings();
        }

        UserInfo a = new UserInfo();

        AssignFlight f = new AssignFlight();

        private void comboTypeRatings()
        {

            MySqlConnection conn = new MySqlConnection(Login.ConnectionString);

            try
            {
                conn.Open();

                string sqlTypeRatings = "select * from typeratings LEFT JOIN typeratingsname ON typeratings.typerating = typeratingsname.id left join flights on typeratingsname.name = flights.aircraft left join utilizadores on flights.departure = utilizadores.location where pilot = @UserID and location=@Location group by name order by typerating";

                MySqlCommand sqlCmd = new MySqlCommand(sqlTypeRatings, conn);
                sqlCmd.Parameters.AddWithValue("@UserID", a.UserID);
                sqlCmd.Parameters.AddWithValue("@Location", a.Location);

                MySqlDataAdapter mysqlDs = new MySqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                mysqlDs.Fill(ds);
                cboxTypeRatings.DataSource = ds.Tables[0];
                cboxTypeRatings.ValueMember = "name";
                cboxTypeRatings.DisplayMember = "name";
            }
            catch (Exception crap)
            {
                MessageBox.Show(crap.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void comboFlightTime(string aircraft)
        {

            MySqlConnection conn = new MySqlConnection(Login.ConnectionString);

            try
            {
                conn.Open();

                string sqlFlightTime = "select HOUR(DATE_ADD(flighttime, INTERVAL 1 HOUR)) as time, SEC_TO_TIME((TIME_TO_SEC(DATE_ADD(flighttime, INTERVAL 1 HOUR))DIV 3600) * 3600) as timevalue from flights left join typeratingsname on flights.aircraft = typeratingsname.name where aircraft = @Aircraft and departure=@Location and HOUR(flighttime) >= 2 group by HOUR(flighttime)";

                MySqlCommand sqlCmd = new MySqlCommand(sqlFlightTime, conn);
                sqlCmd.Parameters.AddWithValue("@Aircraft", aircraft);
                sqlCmd.Parameters.AddWithValue("@Location", a.Location);

                MySqlDataAdapter mysqlDs = new MySqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                mysqlDs.Fill(ds);
                cboxFlightTime.DataSource = ds.Tables[0];
                cboxFlightTime.ValueMember = "timevalue";
                cboxFlightTime.DisplayMember = "time";
            }
            catch (Exception crap)
            {
                MessageBox.Show(crap.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void cboxTypeRatings_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cboxTypeRatings.SelectedValue != null)
                comboFlightTime(cboxTypeRatings.SelectedValue.ToString());
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {

            f.RandAssignFlight(a.Location, cboxTypeRatings.SelectedValue.ToString(), cboxFlightTime.SelectedValue.ToString());

            var b = this.Parent as FlightAssignmentCtrl;

            b.flightAssignedCtrl.UpdateInfos();

            b.flightAssignedCtrl.Show();

            b.AlertFlight();

            this.Hide();

            

        }
    }
}
