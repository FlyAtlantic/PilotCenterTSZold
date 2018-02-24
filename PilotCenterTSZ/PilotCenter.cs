using System;
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
}
