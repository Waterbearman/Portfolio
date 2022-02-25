using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camping_Test
{
    public class DBHelper
    {
        public static MySqlConnection conn = new MySqlConnection();
        public static DataSet ds;
        public static DataTable dt_booking;
        public static DataTable dt_user;

        public static void ConnectDB()
        {
            try
            {

                string connect = string.Format("server={0}; " +
                    "port={1}; Database={2}; Uid={3}; Pwd={4};Charset=utf8",
                    "localhost", "3306", "campingbooking", "root", "1234");
                conn = new MySqlConnection(connect);
                conn.Open();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public static void DB_Check()
        {
            bool temp = conn.Ping();

            if (temp == true)
                System.Windows.Forms.MessageBox.Show("DB 연결 성공");
            else
                System.Windows.Forms.MessageBox.Show("DB 연결 실패");

            conn.Close();
        }

        public static void selectQuery_Booking(string bookingnum = "")
        {
            try
            {
                ConnectDB();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                if (bookingnum == "")
                {
                    cmd.CommandText = "SELECT * FROM booking";

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "booking");

                    dt_booking = ds.Tables[0];
                }
                else
                {
                    cmd.CommandText = "Select * FROM booking where bookingnum = " + bookingnum;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "booking");

                    dt_booking = ds.Tables[0];
                }

                conn.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void selectQuery_User(int Id = -1)
        {
            try
            {
                ConnectDB();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                if (Id < 0)
                {
                    cmd.CommandText = "SELECT * FROM user";

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "user");

                    dt_user = ds.Tables[0];
                }
                else
                {
                    cmd.CommandText = "Select * FROM user where id = " + Id;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "user");

                    dt_user = ds.Tables[0];
                }

                conn.Close();
            }
            catch (Exception e) 
            {

                throw e;
            }
            
        }
        

        public static void InsertUser(string Id, string Name)
        {
            try
            {
                ConnectDB();
                string sqlcommand = "Insert into user(id, name) values (@p1, @p2)";
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection= conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p1", Id);
                cmd.Parameters.AddWithValue("@p2", Name);
                cmd.CommandText=sqlcommand;
                cmd.ExecuteNonQuery(); // 쿼리 실행
                conn.Close();
            }
            catch (Exception e)
            {
                             
            }
        }

        public static void UpdateUser(string Id,string name)
        {
            try
            {
                ConnectDB();
                string sqlcommand = "Update user set name=@p1 where id=@p2";
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p1", name);
                cmd.Parameters.AddWithValue("@p2", Id);
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {

            }
        }

        public static void DeleteUser(string Id)
        {
            try
            {
                ConnectDB();
                string sqlcommand = "Delete from user where id=@p1";
                MySqlCommand cmd = new MySqlCommand();
                
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p1", Id);
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); // 쿼리 실행
                conn.Close ();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void InsertBooking(string bookingnum, string area, DateTime bookingdate, bool checking)
        {
            try
            {
                ConnectDB();
                string sqlcommand = "Insert into booking(bookingnum, area, bookingdate, checking) values (@p1, @p2, @p3)";
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p1", bookingnum);
                cmd.Parameters.AddWithValue("@p2", area);
                cmd.Parameters.AddWithValue("@p3", checking);
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); // 쿼리 실행
                conn.Close();
            }
            catch (Exception e)
            {

            }
        }
        public static void UpdateBooking(string bookingnum, string area)
        {
            
            try
            {
                ConnectDB();
                string sqlcoomand = "Update user set bookingnum = @p1 where area = @p2";
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p1", bookingnum);
                cmd.Parameters.AddWithValue("@p2", area);
                cmd.CommandText = sqlcoomand;
                cmd.ExecuteNonQuery (); //쿼리실행
                conn.Close ();
            }
            catch (Exception)
            {
            }
            
        }
        public static void DeleteBooking(string bookingnum)
        {
            try
            {
                ConnectDB();
                string sqlcommand = "Delete from booking where bookingnum=@p1";
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p1", bookingnum);
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); // 쿼리 실행
                conn.Close();

            }
            catch (Exception)
            {
            }
        }

        public static void allInsertIntoBooking(string bookingnum, string area, DateTime bookingdate, bool checking, string UserId, string username)
        {
            try
            {
                ConnectDB();
                string sqlcommand;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                if (UserId != null && bookingdate != DateTime.MinValue) // 예약 중 
                {
                    sqlcommand = "Insert into booking(bookingnum, area, bookingdate, checking, userid, username) values (@p1, @p2, @p3, @p4, @p5, @p6)";
                    cmd.Parameters.AddWithValue("@p1", bookingnum);
                    cmd.Parameters.AddWithValue("@p2", area);
                    cmd.Parameters.AddWithValue("@p3", bookingdate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@p4", checking);
                    cmd.Parameters.AddWithValue("@p5", UserId);
                    cmd.Parameters.AddWithValue("@p6", username);
                }
                else // 예약 중 이지 않은 상태
                {
                    sqlcommand = "Insert into booking(bookingnum, area, checking) values (@p1, @p2, @p3)";
                    cmd.Parameters.AddWithValue("@p1", bookingnum);
                    cmd.Parameters.AddWithValue("@p2", area);
                    cmd.Parameters.AddWithValue("@p3", checking);
                }
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void allInsertIntoUser(string Id, string Name)
        {
            try
            {
                ConnectDB();
                string sqlcommand = "Insert into user(id, name) values (@p1, @p2)";
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;             
                cmd.Parameters.AddWithValue("@p1", Id);
                cmd.Parameters.AddWithValue("@p2", Name);
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); // 쿼리 실행
                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
 

        public static void allDeleteTable(string tableName)
        {
            try
            {
                ConnectDB();              
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                string sqlcommand = $"Delete from {tableName}";
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); // 쿼리 실행
                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
