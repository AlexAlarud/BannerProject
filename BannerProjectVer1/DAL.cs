using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BannerProjectVer1
{

    //obviously all exeptions should be thrown to an exeption handler for display in frontend and not just console.writeLine
    class DAL
    {
        // connection setup

        ConnectionClass myConnection = new ConnectionClass();

        //Generic Execute Reader
        public SqlDataReader ExecuteReader(string sqlString)
        {
            SqlDataReader myReader = null; //både executeReader o specifika metoder borde kanske inte behöva ha denna rad
            SqlConnection mySqlConnection = myConnection.GetConnection();

            SqlCommand mySqlCommand = new SqlCommand(sqlString, mySqlConnection);

            try
            {
                mySqlConnection.Open(); //reader is closed in each method
                myReader = mySqlCommand.ExecuteReader();
            }
            catch(SqlException sqle)
            {
                Console.WriteLine("ExecuteReader sql exception. " + sqle.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("ExecuteReader exception. " + e.Message);
            }

            return myReader; 

        }

        //Generic execute non query that returns an int for rows affected
        public int ExecuteNonQuery(string sqlString)
        {
            int rowsAffected = 0;
            SqlConnection mySqlConnection = myConnection.GetConnection();

            SqlCommand mySqlCommand = new SqlCommand(sqlString, mySqlConnection);

            try
            {
                mySqlConnection.Open();
                rowsAffected = mySqlCommand.ExecuteNonQuery();
            }
            catch (SqlException sqle)
            {
                Console.WriteLine("ExecuteNonQuery sql exception. " + sqle.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("ExecuteNonQuery exception. " + e.Message);
            }

            mySqlConnection.Close();

            return rowsAffected;
        }


        //Read all colors
        public List<Color> ReadAllColors()
        {
            SqlDataReader myReader = null;

            String sqlString = "SELECT * FROM Color";
            myReader = ExecuteReader(sqlString);

            List<Color> colors = new List<Color>();

            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Color temp = new Color();
                        temp.ColorID = myReader.GetString(0);
                        temp.ColorName = myReader.GetString(1);
                        colors.Add(temp);
                    }
                }
                myReader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("ReadAllColors exception. " + e.Message);
            }

            return colors;

        }

        //Read all patterns
        public List<Pattern> ReadAllPatterns()
        {

            String sqlString = "SELECT * FROM Pattern";     //could do select ID and Name also because won't use picture in this program (so far)
            SqlDataReader myReader = ExecuteReader(sqlString);

            List<Pattern> patterns = new List<Pattern>();

            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Pattern temp = new Pattern();
                        temp.PatternID = myReader.GetString(0);
                        temp.PatternName = myReader.GetString(1);
                        //add temp.PatternPicture = myReader.GetString(2); if pictures is to be used
                        patterns.Add(temp);
                    }
                }
                myReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ReadAllPatterns exception. " + e.Message);
            }

            return patterns;
        }


//CRUD Category

        public int CreateCategory(string categoryName)
        {
            int rowsAffected = 0; //this is done in the execute reader too, necessary to do twice?

            string sqlStr = "INSERT INTO Category VALUES('" + categoryName + "')";

            rowsAffected = ExecuteNonQuery(sqlStr);

            return rowsAffected;
        }

        public int UpdateCategory(int categoryID, string categoryName)
        {
            int rowsAffected = 0; //this is done in the execute reader too, necessary to do twice?
            string sqlStr = "UPDATE Category SET categoryName = '" + categoryName + "' WHERE categoryID =" + categoryID;

            rowsAffected = ExecuteNonQuery(sqlStr);

            return rowsAffected;        

        }

        public int DeleteCategory(int categoryID)
        {
            int rowsAffected = 0; //this is done in the execute reader too, necessary to do twice?
            string sqlStr =  "DELETE FROM Category WHERE categoryID =" + categoryID;

            rowsAffected = ExecuteNonQuery(sqlStr);

            return rowsAffected;
        }

        //Read All Categorys for before I've done the automatic handle of category 1-7

        public List<Category> ReadAllCategorys()
        {
            SqlDataReader myReader = null;

            String sqlString = "SELECT * FROM Category";
            myReader = ExecuteReader(sqlString);

            List<Category> categorys = new List<Category>();

            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Category temp = new Category();
                        temp.CategoryID = myReader.GetInt32(0);
                        temp.CategoryName = myReader.GetString(1);
                        categorys.Add(temp);
                    }
                }
                myReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ReadAllCategorys exception. " + e.Message);
            }

            return categorys;
        }

        //Read All Selectable Categorys - because categorys 1-7 should be handled automatically, and should not be possible to edit or delete from this program

        public List<Category> ReadAllSelectableCategorys()
        {
            SqlDataReader myReader = null;

            String sqlString = "SELECT * FROM Category WHERE categoryID > 7"; 
            myReader = ExecuteReader(sqlString);

            List<Category> categorys = new List<Category>();

            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Category temp = new Category();
                        temp.CategoryID = myReader.GetInt32(0);
                        temp.CategoryName = myReader.GetString(1);
                        categorys.Add(temp);
                    }
                }
                myReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ReadAllSelectableCategorys exception. " + e.Message);
            }

            return categorys;
        }

//CRUD Banner
    
        public int CreateBanner(string bannerName, string bannerColor, string bannerPicture, int bannerCategoryId, List<int> secondaryCategoryIDs, List<BannerStep> bannerSteps)
        {
            int rowsAffected = 0;

            string sqlStr1 = "INSERT INTO Banner (bannerName, bannerColorID, bannerPicture, primaryBannerCategory)" +
                "VALUES ('" + bannerName + "' , '" + bannerColor + "' , '" + bannerPicture + "' , " + bannerCategoryId + ")";
                

            rowsAffected = ExecuteNonQuery(sqlStr1);

            //somwhere after here someting seem to go wrong

            if (rowsAffected == 1)
            {
                SqlDataReader myReader = null;

                string sqlStr2 = "SELECT IDENT_CURRENT('Banner')";   //Gets the auto generated id for the newly created banner so that we can use it to create the rest
                myReader = ExecuteReader(sqlStr2);

                int bannerID = 0;

                try
                {
                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            bannerID = Convert.ToInt32(myReader.GetValue(0)); //omg this finaly works
                        }
                    }
                    myReader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("CreateBanner get banner id exception. " + e.Message);
                }

                if (bannerID != 0)
                {
                    //here it should be confirmd the banner is created in the database, kinda do by checking bannerID isnt 0 
                    
                    //to create the secondary categorys
                    foreach (int id in secondaryCategoryIDs)
                    {
                        //rowsAffected = 0;

                        string sqlStr3 = "INSERT INTO SecondaryBannerCategory VALUES (" + bannerID + " , " + id + ")";

                        ExecuteNonQuery(sqlStr3);
                        //could add a check of rows affected to see it worked
                    }

                    //to create the banner steps
                    foreach (BannerStep stp in bannerSteps)
                    {
                        int stpNumber = stp.BannerStepNumber;
                        string stpColor = stp.BannerStepColorID;
                        string stpPattern = stp.BannerStepPatternID;

                        string sqlStr4 = "INSERT INTO BannerPatternStep VALUES (" + bannerID + " , "  + stpNumber + " , '"
                            + stpColor + "' , '" + stpPattern + "')";

                        ExecuteNonQuery(sqlStr4);

                        //could add a check of rows affected to see it worked here too
                    }

                }
                else
                {
                    //throw exception
                }
            }
            else
            {
                //throw error
            }

            return rowsAffected; //it would be cool if it returnd a sting with the banner name and number of steps, to wite out as succes messege
        }


    }
}
