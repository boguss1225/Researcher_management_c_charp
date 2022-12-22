using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Model;
using Controller;


namespace Database
{
    class ERDAdapter
    {
        private static bool reportingErrors = false;

        //Connect to the database
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        //Converting strings to enums is so horribly broken
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }


        //connection to the database.
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        //the function of loading the reseracher table.
        public static List<Researcher> LoadAll()
        {
            List<Researcher> staff = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(" SELECT id, given_name, family_name, title, photo, campus, IFNULL(level,'Student'), unit, email, current_start, utas_start  from researcher", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Retrive the data from researcher table
                    EmploymentLevel lvl = ParseEnum<EmploymentLevel>(rdr.GetValue(6).ToString());
                    staff.Add(new Researcher
                    {
                        ID = rdr.GetInt32(0),
                        GivenName = rdr.GetString(1),
                        FamilyName = rdr.GetString(2),
                        Title = rdr.GetString(3),
                        photo = rdr.GetString(4),
                        Campus = rdr.GetString(5),
                        Level = lvl,
                        Unit = rdr.GetString(7),
                        Email = rdr.GetString(8),
                        CurrentDate = rdr.GetDateTime(9),
                        StartDate = rdr.GetDateTime(10)
                       

                    });
                }
            }

            catch (MySqlException e)  //get exception situation
            {
                ReportError("loading staff", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return staff;
        }

        //Using switch to identify the level
        public static string GtLevelName(string level)
        {

            string title = "";

            switch (level)
            {
                case (string)("Student"):
                    title = "Student";
                    break;
                case (string)("A"):
                    title = "PostDoc";
                    break;
                case (string)("B"):
                    title = "Lecturer";
                    break;
                case (string)("C"):
                    title = "Senior";
                    break;
                case (string)("D"):
                    title = "Associate Professor";
                    break;
                case (string)("E"):
                    title = "Professor";
                    break;
            }
            return title;

        }

        //Retrive data from position table.
        public static List<Position> LoadPosition(int id)
        {
            List<Position> pos = new List<Position>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select level, Start, End from position where end is not null and id=?id", conn);


                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    pos.Add(new Position
                    {
                        level = ParseEnum<EmploymentLevel>(rdr.GetString(0)),
                        StartDate = rdr.GetDateTime(1),
                        EndDate = rdr.GetDateTime(2),
                        levelName = GtLevelName(rdr.GetString(0))

                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading postion", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return pos;
        }

        //Retrive data from Publication table.
        public static List<Publication> LoadPublication(int id)
        {
            List<Publication> pub = new List<Publication>();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select p.DOI, Title, Authors, year, type, cite_as, available from publication as p, researcher_publication as resp where p.doi=resp.doi and researcher_id=?id order by year desc, Title asc", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    pub.Add(new Publication
                    {
                        DOI = rdr.GetString(0),
                        Title = rdr.GetString(1),
                        Authors = rdr.GetString(2),
                        Year = rdr.GetString(3),
                        Pubtype = ParseEnum<Pubtype>(rdr.GetString(4)),
                        Cite_as = rdr.GetString(5),
                        AvailableDate = rdr.GetDateTime(6),

                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading Publication", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return pub;
        }

        //Retrive the name for supervisor through their ID
        public static string GetName(int sid)
        {
            string sname = "null";

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select given_name, family_name from researcher where id=?sid", conn);
                cmd.Parameters.AddWithValue("sid", sid);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    sname = rdr.GetString(0) + " " + rdr.GetString(1);
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading Publication", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return sname;
        }

        //Retrive data for supervisors
        public static Staff LoadStaff(int id)
        {
            Staff sta = new Staff();
            List<Researcher> students = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand("SELECT degree, supervisor_id from researcher where level is null and researcher.id=?id", conn);
                MySqlCommand cmd = new MySqlCommand("select te.id, st.id, st.given_name, st.family_name from researcher te, researcher st where te.id = st.supervisor_id and te.id=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    students.Add(new Researcher { GivenName = rdr.GetString(2), FamilyName = rdr.GetString(3) });


                }
            }
            catch (MySqlException e)
            {
                ReportError("loading work", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            sta.Student = students;

            return sta;

        }


        public static Student LoadStudent(int id)
        {
            Student stu = new Student();
            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand("SELECT degree, supervisor_id from researcher where level is null and researcher.id=?id", conn);
                MySqlCommand cmd = new MySqlCommand("SELECT degree, supervisor_id from researcher where researcher.id=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    {
                        stu.Degree = rdr.GetString(0);
                        stu.SupervisorID = rdr.GetInt32(1);

                    };
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading work", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return stu;
        }

        //Resttrive data from publication table in order to Cumulate publication information
        public static List<PublicationCumulative> PublicationCumulative(int id)
        {
            List<PublicationCumulative> cum = new List<PublicationCumulative>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select r.id,p.year, count(p.doi) "
                                                + "from researcher r, researcher_publication rp, publication p "
                                                + "where r.id = rp.researcher_id and p.doi = rp.doi and r.id=?id "
                                                + "group by r.id, p.year", conn);


                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cum.Add(new PublicationCumulative
                    {

                        Year = rdr.GetInt32(1),
                        Count = rdr.GetInt32(2)

                    });

                }
            }
            catch (MySqlException e)
            {
                ReportError("loading postion", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return cum;
        }

        private static void ReportError(string msg, Exception e)
        {
            if (reportingErrors)
            {
                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
