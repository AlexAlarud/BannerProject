using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerProjectVer1
{
    class ConnectionClass
    {

        public SqlConnection GetConnection()
        {
            try
            {
                string connectionString = "server=localhost; Trusted_Connection=yes; database=BannerProject;";

                SqlConnection mySqlConnection = new SqlConnection(connectionString);

                return mySqlConnection;
            }

            catch(SqlException sqle)
            {
                Console.WriteLine("Whops, SQL!" + sqle.Message); //to be changed later (to throw)

                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine("Whops!" + e.Message); //to be changed later (to throw)

                return null;
            }
        }
    }
}
