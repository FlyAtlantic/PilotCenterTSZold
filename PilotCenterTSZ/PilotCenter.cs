﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamCenterTSZ.Functions;
using MySql.Data.MySqlClient;
using Dapper;

namespace PilotCenterTSZ
{
    public class UserInfo
    {
        public int UserID
        { get; set; }

        public string UserName
        { get; set; }

        public string UserSurname
        { get; set; }

        public string Rank
        { get; set; }

        public int RankID
        { get; set; }

        public string Rate
        { get; set; }

        public int Callsign
        { get; set; }

        public TimeSpan PilotHours
        { get; set; }

        public DateTime LastFlight
        { get; set; }

        public string Hub
        { get; set; }

        public string Location
        { get; set; }

        public int Eps
        { get; set; }

        public UserInfo()
        {
            string sqlPilotInformations = "SELECT user_id, user_nome, user_apelido, ranks.rank, ratings.ratingname, utilizadores.callsign, utilizadores.pilot_hours, pireps.date, hubs.icao, utilizadores.location, utilizadores.eps, ranks.rankid from utilizadores left join ratings on utilizadores.rate = ratings.id left join ranks on utilizadores.rank = ranks.rankid left join pireps on utilizadores.user_id = pireps.pilotid LEFT JOIN flights ON pireps.flightid = flights.idf left join hubs on utilizadores.hub = hubs.id where user_email=@Email order by pireps.date desc LIMIT 1";
            MySqlConnection conn = new MySqlConnection(Login.ConnectionString);

            try
            {
                conn.Open();

                MySqlCommand sqlCmd = new MySqlCommand(sqlPilotInformations, conn);
                sqlCmd.Parameters.AddWithValue("@Email", Properties.Settings.Default.Email);

                MySqlDataReader sqlCmdRes = sqlCmd.ExecuteReader();
                if (sqlCmdRes.HasRows)
                    while (sqlCmdRes.Read())
                    {
                        UserID = (int)sqlCmdRes[0];
                        UserName = (string)sqlCmdRes[1];
                        UserSurname = (string)sqlCmdRes[2];
                        Rank = (string)sqlCmdRes[3];
                        Rate = (string)sqlCmdRes[4];
                        Callsign = (int)sqlCmdRes[5];
                        PilotHours = TimeSpan.FromMinutes((float)sqlCmdRes[6]);
                        LastFlight = (DateTime)sqlCmdRes[7];
                        Hub = (string)sqlCmdRes[8];
                        Location = (string)sqlCmdRes[9];
                        Eps = (int)sqlCmdRes[10];
                        RankID = (int)sqlCmdRes[11];
                    }

            }
            catch (Exception crap)
            {
                throw new ApplicationException("Failed to load exam @UserInfo()", crap);
            }
            finally
            {

                conn.Close();
            }
        }
    }

    public class TypeRating
    {
        public string TypeRatingName
        { get; set; }

        public DateTime Validity
        { get; set; }

        public DateTime Expiration
        { get; set; }

        public TypeRating() { }

        public static List<TypeRating> Get()
        {
            return new MySqlConnection(Login.ConnectionString).Query<TypeRating>(
                @"
SELECT
    typeratingsname.name as TypeRatingName,
    typeratings.validity as Validity,
    typeratings.expire as Expiration
from typeratings
left join utilizadores
    on typeratings.pilot = utilizadores.user_id
left join typeratingsname
    on typeratings.typerating = typeratingsname.id
where user_email=@Email",
                new
                {
                    Email = Properties.Settings.Default.Email
                }).ToList();
        }
    }

    public class Qualification
    {
        public string QualificationName
        { get; set; }

        public DateTime Validity
        { get; set; }

        public DateTime Expiration
        { get; set; }

        public Qualification() { }

        public static List<Qualification> Get()
        {
            return new MySqlConnection(Login.ConnectionString).Query<Qualification>(
                @"
SELECT
    qualificationsname.name as QualificationName,
    qualifications.validity as Validity,
    qualifications.expire as Expiration
from qualifications
left join utilizadores
    on qualifications.pilot = utilizadores.user_id
left join qualificationsname
    on qualifications.qualification = qualificationsname.id
where user_email=@Email and qualification != 0",
                new
                {
                    Email = Properties.Settings.Default.Email
                }).ToList();
        }
    }

    public class UserOverallEfficiency
    {
        public int PirepsCount
        { get; set; }

        public int PirepsCountSum
        { get; set; }

        public int Efficiency
        { get; set; }

        public UserOverallEfficiency()
        {


            string sqlPirepsCount = "SELECT COUNT(*) FROM `pireps` left join utilizadores on user_id = pireps.pilotid WHERE user_email=@Email and accepted = 1";
            string sqlPirepsCountSum = "SELECT SUM(sum) FROM `pireps` left join utilizadores on user_id = pireps.pilotid WHERE user_email=@Email and accepted = 1";
            MySqlConnection conn = new MySqlConnection(Login.ConnectionString);

            try
            {
                conn.Open();

                MySqlCommand sqlCmd = new MySqlCommand(sqlPirepsCount, conn);
                sqlCmd.Parameters.AddWithValue("@Email", Properties.Settings.Default.Email);

                PirepsCount = Convert.ToInt32(sqlCmd.ExecuteScalar());

                MySqlCommand sqlCmd1 = new MySqlCommand(sqlPirepsCountSum, conn);
                sqlCmd1.Parameters.AddWithValue("@Email", Properties.Settings.Default.Email);

                PirepsCountSum = Convert.ToInt32(sqlCmd1.ExecuteScalar());


                Efficiency = PirepsCountSum / PirepsCount;
            }
            catch (Exception crap)
            {
                throw new ApplicationException("Failed to load exam @UserInfo()", crap);
            }
            finally
            {

                conn.Close();
            }
        }
    }

    public class UserHourAward
    {
        public int AwardID
        { get; set; }

        public int AwardMinHours
        { get; set; }

        public int AwardEps
        { get; set; }

        public UserHourAward()
        {


            string sqlAwardID = "Select hour_award from utilizadores where user_email = @Email";

            MySqlConnection conn = new MySqlConnection(Login.ConnectionString);

            try
            {
                conn.Open();

                MySqlCommand sqlCmd = new MySqlCommand(sqlAwardID, conn);
                sqlCmd.Parameters.AddWithValue("@Email", Properties.Settings.Default.Email);

                MySqlDataReader sqlCmdRes = sqlCmd.ExecuteReader();
                if (sqlCmdRes.HasRows)
                    while (sqlCmdRes.Read())
                    {
                        AwardID = (int)sqlCmdRes[0];

                        if (AwardID == 0)
                        {
                            AwardMinHours = 25;
                            AwardEps = 25;
                        }
                        else if (AwardID == 1)
                        {
                            AwardMinHours = 50;
                            AwardEps = 50;
                        }
                        else if (AwardID == 2)
                        {
                            AwardMinHours = 75;
                            AwardEps = 75;
                        }
                        else if (AwardID == 3)
                        {
                            AwardMinHours = 100;
                            AwardEps = 100;
                        }
                    }
            }
            catch (Exception crap)
            {
                throw new ApplicationException("Failed to load exam @UserHourAward()", crap);
            }
            finally
            {

                conn.Close();
            }
        }

        public static void SendAwardEps(int awardID, int awardEps)
        {
            string sqlSendAwardEps = "Update utilizadores SET hour_award = @AwardID, eps = eps + @Eps where user_email = @Email LIMIT 1";

            MySqlConnection conn = new MySqlConnection(Login.ConnectionString);

            try
            {
                conn.Open();

                MySqlCommand sqlCmd = new MySqlCommand(sqlSendAwardEps, conn);
                sqlCmd.Parameters.AddWithValue("@Email", Properties.Settings.Default.Email);
                sqlCmd.Parameters.AddWithValue("@AwardID", awardID + 1);
                sqlCmd.Parameters.AddWithValue("@Eps", awardEps);

                sqlCmd.ExecuteScalar();
            }
            catch (Exception crap)
            {
                throw new ApplicationException("Failed to load exam @SendAwardEps()", crap);
            }
            finally
            {

                conn.Close();
            }
        }
    }

    public class UserStatistics
    {
        public int Day
        { get; set; }

        public int NumFlights
        { get; set; }

        public UserStatistics()
        {

        }

        public static List<UserStatistics> Get()
        {
            return new MySqlConnection(Login.ConnectionString).Query<UserStatistics>(
                @"
select 
    DAY(date) as Day, COUNT(date) as NumFlights
from
    pireps 
left join
    utilizadores
on
    pireps.pilotid = utilizadores.user_id
where 
    accepted = 1 
and 
    user_email=@Email 
and 
    MONTH(date) = MONTH(CURRENT_DATE()) 
group by 
    DAY(date)",
                new
                {
                    Email = Properties.Settings.Default.Email
                }).ToList();
        }
    }

    public class AssignFlight
    {
        public static int UserID
        { get; set; }

        public int FlightID
        { get; set; }

        public string FlightCallsign
        { get; set; }

        public string UserDeparture
        { get; set; }

        public string UserArrival
        { get; set; }

        public string UserAircraft
        { get; set; }

        public DateTime UserDateAssign
        { get; set; }

        public AssignFlight()
        {

        }

        public void VerifyFlightAssign()
        {
            string sqlVerifyFlightAssign = "select departure, destination, aircraft, idf, user_id, date_assigned, flights.callsign from pilotassignments LEFT JOIN flights ON pilotassignments.flightid = flights.idf left join utilizadores on pilotassignments.pilot = utilizadores.user_id where user_email=@Email LIMIT 1";
            MySqlConnection conn = new MySqlConnection(Login.ConnectionString);

            try
            {
                conn.Open();

                MySqlCommand sqlCmd = new MySqlCommand(sqlVerifyFlightAssign, conn);
                sqlCmd.Parameters.AddWithValue("@Email", Properties.Settings.Default.Email);

                MySqlDataReader sqlCmdRes = sqlCmd.ExecuteReader();
                if (sqlCmdRes.HasRows)
                    while (sqlCmdRes.Read())
                    {
                        UserID = (int)sqlCmdRes[4];
                        FlightID = (int)sqlCmdRes[3];
                        UserDeparture = (string)sqlCmdRes[0];
                        UserArrival = (string)sqlCmdRes[1];
                        UserAircraft = (string)sqlCmdRes[2];
                        UserDateAssign = (DateTime)sqlCmdRes[5];
                        FlightCallsign = (string)sqlCmdRes[6];
                    }
                
            }
            catch (Exception crap)
            {
                throw new ApplicationException("Failed to load exam @UserInfo()", crap);
            }
            finally
            {
                Dashboard.GetFlightInfosToDash(FlightID, FlightCallsign, UserDeparture, UserArrival, UserAircraft);
                conn.Close();
            }
        }

        public void RandAssignFlight(string userLocation, string aircraft, string flightTime)
        {
            string sqlRandAssignFlight = "select departure, destination, aircraft, idf, user_id  from qualifications inner join flights on qualifications.qualification = flights.qualification_need left join utilizadores on qualifications.pilot = utilizadores.user_id where user_email = @Email and aircraft=@Aircraft and flighttime<=@FlightTime and departure =@Location ORDER BY RAND() LIMIT 1";
            string sqlInsertAssign = "INSERT INTO `pilotassignments` (`flightid`, `pilot`, `date_assigned`) VALUES (@FlightID, @UserID, NOW())";
            MySqlConnection conn = new MySqlConnection(Login.ConnectionString);

            try
            {
                conn.Open();

                MySqlCommand sqlCmd = new MySqlCommand(sqlRandAssignFlight, conn);
                sqlCmd.Parameters.AddWithValue("@Email", Properties.Settings.Default.Email);
                sqlCmd.Parameters.AddWithValue("@Aircraft", aircraft);
                sqlCmd.Parameters.AddWithValue("@FlightTime", flightTime);
                sqlCmd.Parameters.AddWithValue("@Location", userLocation);

                MySqlDataReader sqlCmdRes = sqlCmd.ExecuteReader();
                if (sqlCmdRes.HasRows)
                {
                    while (sqlCmdRes.Read())
                    {
                        UserID = (int)sqlCmdRes[4];
                        FlightID = (int)sqlCmdRes[3];
                        UserDeparture = (string)sqlCmdRes[0];
                        UserArrival = (string)sqlCmdRes[1];
                        UserAircraft = (string)sqlCmdRes[2];
                    }
                    conn.Close();
                    conn.Open();
                    MySqlCommand sqlCmd1 = new MySqlCommand(sqlInsertAssign, conn);
                    sqlCmd1.Parameters.AddWithValue("@FlightID", FlightID);
                    sqlCmd1.Parameters.AddWithValue("@UserID", UserID);

                    sqlCmd1.ExecuteScalar();
                }
            }
            catch (Exception crap)
            {
                throw new ApplicationException("Failed to load exam @UserInfo()", crap);
            }
            finally
            {

                conn.Close();
            }
        }

        public static void VerifyFlightTimeOut()
        {

            string sqlDeleteAssignment = "DELETE FROM pilotassignments WHERE pilot=@UserID and NOW() >=  DATE_ADD(date_assigned, INTERVAL 30 MINUTE) LIMIT 1";
            MySqlConnection conn = new MySqlConnection(Login.ConnectionString);

            try
            {
                conn.Open();

                MySqlCommand sqlCmd = new MySqlCommand(sqlDeleteAssignment, conn);
                sqlCmd.Parameters.AddWithValue("@UserID", UserID);

                sqlCmd.ExecuteScalar();

            }
            catch (Exception crap)
            {
                throw new ApplicationException("Failed to load exam @sqlDeleteAssignment()", crap);
            }
            finally
            {

                conn.Close();
            }
        }        

    }
}

