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
        public static MySqlConnection conn = new MySqlConnection("Server=localhost;" +
        "Database=campingbooking; Uid=root; Pwd=1234;");
        public static DataSet ds;
        public static DataTable dt_booking;
        public static DataTable dt_user;

        //public static void ConnectDB()
        //{
        //    string server = "localhost";
        //    int port = 3306;
        //    string database = "campingbooking";
        //    string id = "root";
        //    string pw = "1234";
        //    string ConnectionString = "";

        //    conn.ConnectionString = String.Format("server=({0}); " +
        //        "port = {1}; Database={2}; Uid={3}; Pwd={4}",
        //        server, port, database, id, pw);
        //    conn = new MySqlConnection(conn.ConnectionString);
        //    conn.Open();  
        //}
        public static void DB_Check()
        {
            bool temp = conn.Ping();

            if (temp == true)
                System.Windows.Forms.MessageBox.Show("DB 연결 성공");
            else
                System.Windows.Forms.MessageBox.Show("DB 연결 실패");

            conn.Close();
        }

        public static void selectQuery_User(int Id = -1)
        {
            //ConnectDB();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            if( Id < 0)
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
                ds= new DataSet();
                da.Fill(ds, "user");

                dt_user = ds.Tables[0];
            }

            conn.Close();
        }

        public static void InsertUser(string Id, string Name)
        {
            try
            {
                //ConnectDB();
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
                //ConnectDB();
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
                
            }
        }

        public static void allInsertIntoUser(string Id, string Name)
        {
            try
            {
                //ConnectDB();
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

            }
        }

        public static void allDeleteTable(string tableName)
        {
            try
            {
                //ConnectDB();              
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                string sqlcommand = $"Delete {tableName}";
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery(); // 쿼리 실행
                conn.Close();
            }
            catch (Exception e)
            {
            }
        }
    }
}
